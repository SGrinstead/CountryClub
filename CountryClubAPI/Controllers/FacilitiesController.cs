using CountryClubAPI.DataAccess;
using CountryClubAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace CountryClubAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacilitiesController : ControllerBase
    {
        private readonly CountryClubContext _context;

        public FacilitiesController(CountryClubContext context)
        {
            _context = context;
        }

        public IActionResult AllFacilities()
        {
            var facilities = _context.Facilities;

            return new JsonResult(facilities);
        }

        [Route("{id}")]
        public IActionResult Facility(int id)
        {
            var facility = _context.Facilities.Find(id);

            if (facility is null)
            {
                return NotFound();
            }

            return new JsonResult(facility);
        }
    }
}
