using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCCS.DatabaseCitizenResidency.Data;
using RCCS.DatabaseCitizenResidency.Model;

namespace RCCS.DatabaseAPI.RCCSCitizenResidencyDbControllers
{
    [Route("rccsdb/[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        private readonly RCCSContext _context;

        public CitizenController(RCCSContext context)
        {
            _context = context;
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
    }
}
