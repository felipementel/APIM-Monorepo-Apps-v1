using DEPLOY.CarApp.API.Domain;
using DEPLOY.CarApp.API.Infra.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPLOY.CarApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsBugsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly Context _context;

        public CarsBugsController(
            ILogger<CarsController> logger, 
            Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("time/{time}")]
        public async Task<IActionResult> GetTime(int time)
        {
            Thread.Sleep(TimeSpan.FromSeconds(time));

            return Ok();
        }

        [HttpGet("size/{size}")]
        public async Task<IActionResult> GetSize(int size)
        {

            byte[] data = new byte[size * (1024 * 1024)];

            return Ok(data);
        }
    }
}
