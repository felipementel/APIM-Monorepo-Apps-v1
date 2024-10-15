using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace DEPLOY.BikeApp.Function
{
    public class Bikes
    {
        private readonly ILogger<Bikes> _logger;

        public Bikes(ILogger<Bikes> logger)
        {
            _logger = logger;
        }

        [Function("bikes")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            if (req.Method == "GET")
                return new OkObjectResult("GET Welcome to Azure Functions!");
            else if (req.Method == "POST")
                return new CreatedResult(string.Empty, "POST Welcome to Azure Functions!");
            else
                return new StatusCodeResult(StatusCodes.Status405MethodNotAllowed);
        }
    }
}
