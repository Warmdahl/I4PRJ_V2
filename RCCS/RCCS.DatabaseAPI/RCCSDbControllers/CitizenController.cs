using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.Model;

namespace RCCS.RCCSDbControllers
{
    [Route("rccsdb/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Coordinator")]
    public class CitizenController : ControllerBase
    {
        private readonly RCCSContext _context;

        public CitizenController(RCCSContext context)
        {
            _context = context;
        }

        // GET: api/Citizen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Citizen>>> GetCitizens()
        {
            return await _context.Citizens.ToListAsync();
        }

        // GET: api/Citizen/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Citizen>> GetCitizen(long id)
        {
            var citizen = await _context.Citizens.FindAsync(id);

            if (citizen == null)
            {
                return NotFound();
            }

            return citizen;
        }

        // PUT: api/Citizen/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCitizen(long id, Citizen citizen)
        {
            if (id != citizen.CPR)
            {
                return BadRequest();
            }

            _context.Entry(citizen).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CitizenExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: rccsdb/Citizen/5
        [HttpDelete("{cpr}")]
        public async Task<ActionResult<Citizen>> DeleteCitizen(long cpr)
        {
            var citizen =
                await _context.Citizens
                    .Include(c => c.RespiteCareRoom)
                        .ThenInclude(rcr => rcr.RespiteCareHome)
                    .SingleOrDefaultAsync(c => c.CPR == cpr);

            if (citizen == null)
            {
                return NotFound();
            }

            citizen.RespiteCareRoom.IsAvailable = true;

            _context.Entry(citizen.RespiteCareRoom).State = EntityState.Modified;

            citizen.RespiteCareRoom.RespiteCareHome.AvailableRespiteCareRooms =
                citizen.RespiteCareRoom.RespiteCareHome.AvailableRespiteCareRooms + 1;

            _context.Entry(citizen.RespiteCareRoom.RespiteCareHome).State = EntityState.Modified;

            _context.Citizens.Remove(citizen);
            await _context.SaveChangesAsync();

            return citizen;
        }

        private bool CitizenExists(long id)
        {
            return _context.Citizens.Any(e => e.CPR == id);
        }
    }
}
