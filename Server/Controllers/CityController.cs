#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FakeCitySite.Server.Data;
using FakeCitySite.Shared;

namespace FakeCitySite.Server.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly CityContext _context;
        public CityController(CityContext context)
        {
            _context = context;
        }

        // GET: api/cities/pair
        [HttpGet("getpair")]
        public City[] GetCities()
        {
            var pair = new City[2];
            pair[0] = _context.Cities.AsNoTracking().Where(x => x.IsReal).AsEnumerable().OrderBy(_ => Guid.NewGuid()).First();
            pair[1] = _context.Cities.AsNoTracking().Where(x => !x.IsReal).AsEnumerable().OrderBy(_ => Guid.NewGuid()).First();
            return pair;
        }
        
        [HttpPatch("incrementtimeschosen/{id}")]
        public async Task<IActionResult> IncrementTimesChosen(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city is null)
            {
                return NotFound();
            }
            city.TimesChosen++;
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
            return Ok(city);
        }
    }
}
