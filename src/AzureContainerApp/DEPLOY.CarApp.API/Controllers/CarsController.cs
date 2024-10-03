using DEPLOY.CarApp.API.Domain;
using DEPLOY.CarApp.API.Infra.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DEPLOY.CarApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly ILogger<CarsController> _logger;
        private readonly Context _context;

        public CarsController(
            ILogger<CarsController> logger, 
            Context context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await _context.Cars.ToListAsync();

            if (cars == null)
            {
                return NotFound();
            }

            return Ok(cars);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Car car)
        {
            if (car == null)
            {
                return BadRequest();
            }

            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = car.Id }, car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Car car)
        {
            var existingCar = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCar == null)
            {
                return NotFound();
            }

            _context.Entry(existingCar).CurrentValues.SetValues(car);
            _context.Update(existingCar);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(x => x.Id == id);

            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
