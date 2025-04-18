using CanadianCitiesAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CanadianCitiesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanadianStatesController : ControllerBase
    {
        private readonly ApiDbContext dbContext;
        public CanadianStatesController(ApiDbContext apiContext) 
        {
            dbContext = apiContext;

            // ensures the database and associated table is created if not already created.
            dbContext.Database.EnsureCreated();
        }

        // GET: api/<CanadianStatesController>
        [HttpGet]
        [Route("Cities/{limit?}")]
        public async Task<ActionResult> GetCities(int limit = 0)
        {
            if (limit > 0) 
            {
                return Ok(await dbContext.Cities.Take(limit).ToListAsync());
            }

            return Ok(await dbContext.Cities.ToListAsync());
        }

        [HttpGet]
        [Route("Provinces")]
        public async Task<ActionResult> GetProvinces()
        {
            return Ok(await dbContext.Provinces.ToListAsync());
        }

        /// <summary>
        /// Returns a list of json objects containing the cities for the given province
        /// </summary>
        /// <param name="provinceName">Name of province in full, no abbreviations.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("CitiesByProvince/{provinceName}")]
        public async Task<ActionResult> GetCitiesByProvince(string provinceName)
        {
            if (string.IsNullOrEmpty(provinceName)) 
            {
                return BadRequest("province name cannot be empty!!");
            }

            Province province = await dbContext.Provinces.Where(x => string.Equals(x.Name, provinceName, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();

            if (province == null) 
            {
                return NotFound("Province does not exist in data source. Please verify and try again");
            }

            var cities = await dbContext.Cities.Where(x => x.ProvinceId == province.Id).ToListAsync();

            return Ok(cities);
        }


    }
}
