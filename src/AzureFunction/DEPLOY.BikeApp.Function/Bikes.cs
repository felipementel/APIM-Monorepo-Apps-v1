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
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
