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
    public class BookingsController : ControllerBase
    {
        private readonly CountryClubContext _context;

        public BookingsController(CountryClubContext context)
        {
            _context = context;
        }

        public IActionResult AllBookings()
        {
            var bookings = _context.Bookings;

            return new JsonResult(bookings);
        }

        [Route("{id}")]
        public IActionResult Booking(int id)
        {
            var booking = _context.Bookings.Find(id);

            if(booking is null)
            {
                return NotFound();
            }

            return new JsonResult(booking);
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking booking)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Bookings.Add(booking);
            _context.SaveChanges();

            Response.StatusCode = 201;
            return new JsonResult(booking);
        }

        [Route("today")]
        public IActionResult BooksingsToday()
        {
            var today = DateTime.Today.ToUniversalTime();
            var tomorrow = DateTime.Today.AddDays(1).ToUniversalTime();

            var bookingsToday = _context.Bookings
                .Where(booking =>
                booking.StartTime.ToUniversalTime() > today
                && booking.StartTime.ToUniversalTime() < tomorrow)
                .ToList();

            return new JsonResult(bookingsToday);
        }

        [Route("thisweek")]
        public IActionResult BookingsThisWeek()
        {
            var today = DateTime.Today.ToUniversalTime();
            var weekFromNow = DateTime.Today.AddDays(8).ToUniversalTime();

            var bookingsThisWeek = _context.Bookings
                .Where(booking =>
                booking.StartTime.ToUniversalTime() > today && booking.StartTime.ToUniversalTime() < weekFromNow)
                .ToList();

            return new JsonResult(bookingsThisWeek);
        }
    }
}
