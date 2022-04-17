#nullable disable
using FakeCitySite.Server.Data;
using FakeCitySite.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // GET: api/cities/getpair
        [HttpGet("getpair")]
        public City[] GetCityPair()
        {
            var pair = new City[2];
            pair[0] = _context.Cities.AsNoTracking().Where(x => x.IsReal).AsEnumerable().OrderBy(_ => Guid.NewGuid()).First();
            pair[1] = _context.Cities.AsNoTracking().Where(x => !x.IsReal).AsEnumerable().OrderBy(_ => Guid.NewGuid()).First();
            return pair;
        }

        [HttpGet("top10fake")]
        public IEnumerable<City> GetTopFakes()
        {
            return _context.Cities.AsNoTracking().Where(x => !x.IsReal).OrderByDescending(x => x.TimesChosen).Take(10);
        }

        [HttpGet("top10real")]
        public IEnumerable<City> GetTopReals()
        {
            return _context.Cities.AsNoTracking().Where(x => x.IsReal).OrderByDescending(x => x.TimesChosen).Take(10);
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
