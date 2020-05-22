//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using RCCS.UsersDatabase.Data;
//using RCCS.UsersDatabase.Model.DTOs;
//using RCCS.UsersDatabase.Model.Entities;
//using RCCS.UsersDatabase.Utilities;
//using static BCrypt.Net.BCrypt;

//namespace RCCS.DatabaseAPI.RCCSDbControllers.Security
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    [Authorize(Roles = "Admin")]
//    public class AdminsController : ControllerBase
//    {
//        private readonly ApplicationDbContext _context;
//        private readonly AppSettings _appSettings;

//        public AdminsController(ApplicationDbContext context,
//            IOptions<AppSettings> appSettings)
//        {
//            _context = context;
//            _appSettings = appSettings.Value;
//        }

//        // GET: api/Admins
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<EfAdmin>>> GetAdmins()
//        {
//            return await _context.Admins.ToListAsync();
//        }

//        // GET: api/Admins/5
//        [HttpGet("{id}")]
//        public async Task<ActionResult<EfAdmin>> GetAdmin(long id)
//        {
//            var admin = await _context.Admins.FindAsync(id);

//            if (admin == null)
//            {
//                return NotFound();
//            }

//            return admin;
//        }

//        // PUT: api/Admins/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutAdmin(long id, EfAdmin admin)
//        {
//            if (id != admin.EfAdminId)
//            {
//                return BadRequest();
//            }

//            // Check if new email
//            var old = await _context.Admins.FindAsync(admin.EfAdminId);
//            if (old.PersonaleId != admin.PersonaleId)
//            {
//                // Update account
//                var account = await _context.Users.FindAsync(admin.EfUserId);
//                account.PersonaleId = admin.PersonaleId;
//            }

//            _context.Entry(admin).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!AdminExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Admins
//        [HttpPost]
//        public async Task<ActionResult<EfAdmin>> PostAdmin(Admin adminDto)
//        {
//            if (adminDto == null)
//                return BadRequest("Data is missing");
//            var admin = new EfAdmin();
//            admin.PersonaleId = adminDto.PersonaleId.ToLowerInvariant();
//            var emailExist = await _context.Users.Where(u => u.PersonaleId == admin.PersonaleId)
//                .FirstOrDefaultAsync().ConfigureAwait(false);
//            if (emailExist != null)
//            {
//                ModelState.AddModelError("Email", "Email already in use");
//                return BadRequest(ModelState);
//            }
//            admin.FirstName = adminDto.FirstName;
//            admin.LastName = adminDto.LastName;
//            var user = new EfUser()
//            {
//                PersonaleId = admin.PersonaleId,
//                Role = Role.Admin
//            };
//            user.PwHash = HashPassword(adminDto.Password, _appSettings.BcryptWorkfactor);
//            admin.User = user;
//            _context.Users.Add(user);
//            _context.Admins.Add(admin);
//            await _context.SaveChangesAsync();

//            return Created(admin.EfAdminId.ToString(), admin);
//        }

//        // DELETE: api/Admins/5
//        [HttpDelete("{id}")]
//        public async Task<ActionResult<EfAdmin>> DeleteAdmin(long id)
//        {
//            var admin = await _context.Admins.FindAsync(id);
//            if (admin == null)
//            {
//                return NotFound();
//            }
//            var account = await _context.Users.FindAsync(admin.EfUserId);
//            _context.Users.Remove(account);
//            _context.Admins.Remove(admin);
//            await _context.SaveChangesAsync();

//            return admin;
//        }

//        private bool AdminExists(long id)
//        {
//            return _context.Admins.Any(e => e.EfAdminId == id);
//        }
//    }
//}
