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
    public class MembersController : ControllerBase
    {
        private readonly CountryClubContext _context;

        public MembersController(CountryClubContext context)
        {
            _context = context;
        }

        public IActionResult AllMembers()
        {
            var members = _context.Members;

            return new JsonResult(members);
        }

        [Route("{id}")]
        public IActionResult Member(int id)
        {
            var member = _context.Members.Find(id);

            if(member is null)
            {
                return NotFound();
            }

            return new JsonResult(member);
        }

        [HttpPost]
        public IActionResult CreateMember(Member member)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Members.Add(member);
            _context.SaveChanges();

            Response.StatusCode = 201;
            return new JsonResult(member);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateMember(Member member, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            member.Id = id;
            _context.Members.Update(member);
            _context.SaveChanges();

            Response.StatusCode = 204;
            return new JsonResult(member);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteMember(int id)
        {
            var memberToDelete = _context.Members.Find(id);

            if (memberToDelete is null)
            {
                return NotFound();
            }

            _context.Remove(memberToDelete);
            _context.SaveChanges();

            var members = _context.Members;
            Response.StatusCode = 204;
            return new JsonResult(members);
        }
    }
}
