using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RegisterAPI.Models;

namespace RegisterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly RegisterContext _context;
        public CatalogsController(RegisterContext context) => _context = context;

        [HttpGet("provinces")]
        public async Task<ActionResult> GetProvinces()
        {
            var provinces = await _context.Provinces.ToListAsync();
            if(provinces == null)
            {
                return NotFound("No provinces found.");
            }

            return Ok(provinces);
        }

        [HttpGet("highschool/{provinceId}")]
        public async Task<ActionResult> GetHighSchoolsByProvince(int provinceId)
        {
            var highSchools = await _context.HighSchools
                .Where(hs => hs.ProvinceId == provinceId)
                .ToListAsync();
            if (highSchools == null || !highSchools.Any())
            {
                return NotFound($"No high schools found for province ID {provinceId}.");
            }
            return Ok(highSchools);
        }

        [HttpGet("areas")]
        public async Task<ActionResult> GetAreas()
        {
            var areas = await _context.Areas.ToListAsync();
            if (areas == null)
            {
                return NotFound("No areas found.");
            }
            return Ok(areas);
        }

        [HttpGet("aspirations")]
        public async Task<ActionResult> GetAspirations()
        {
            var aspirations = await _context.Aspirations.ToListAsync();
            if (aspirations == null)
            {
                return NotFound("No aspirations found.");
            }
            return Ok(aspirations);
        }

        [HttpGet("majors")]
        public async Task<ActionResult> GetMajors()
        {
            var majors = await _context.Majors.ToListAsync();
            if (majors == null)
            {
                return NotFound("No majors found.");
            }
            return Ok(majors);
        }

    }
}
