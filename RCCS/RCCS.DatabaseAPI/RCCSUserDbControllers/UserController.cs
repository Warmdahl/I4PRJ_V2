using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RCCS.DatabaseUsers.Data;
using RCCS.DatabaseUsers.Model.DTOs;
using RCCS.DatabaseUsers.Model.Entities;
using RCCS.DatabaseUsers.Utilities;
using static BCrypt.Net.BCrypt;

namespace RCCS.DatabaseAPI.RCCSDbControllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly RCCSUsersContext _context;
        private readonly AppSettings _appSettings;

        public UserController(RCCSUsersContext context)
        {
            _context = context;
        }

        public UserController(RCCSUsersContext context,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }


        // POST: api/user/login
        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<Token>> Login([FromBody] Login login)
        {
            Console.WriteLine("LoginController activated");
            if (login != null) //check if email, username and password were entered
            {
                //find user by personaleId
                login.PersonaleId = login.PersonaleId.ToLowerInvariant();
                var user = await _context.Users.Where(u => u.PersonaleId == login.PersonaleId)
                    .FirstOrDefaultAsync().ConfigureAwait(false);
                if (user != null)
                {
                    var validPwd = Verify(login.Password, user.PwHash);
                    if (validPwd)
                    {
                        Console.WriteLine("Password valid");
                        long userID = 0;
                        switch (user.Role)
                        {
                            case Role.Admin:
                                {
                                    Console.WriteLine("User logging in is Admin");
                                    var admin = await _context.Admins.Where(m => m.EfUserId == user.EfUserId)
                                        .FirstOrDefaultAsync().ConfigureAwait(false);
                                    if (admin != null)
                                    {
                                        userID = admin.EfAdminId;
                                    }
                                    break;
                                }
                            case Role.Coordinator:
                                {
                                    Console.WriteLine("User logging in is Coordinator");
                                    var coordinator = await _context.Coordinators.Where(m => m.EfUserId == user.EfUserId)
                                        .FirstOrDefaultAsync().ConfigureAwait(false);
                                    if (coordinator != null)
                                    {
                                        userID = coordinator.EfCoordinatorId;
                                    }

                                    break;
                                }
                            case Role.NursingStaff:
                                {
                                    Console.WriteLine("User logging in is Nursing Staff");
                                    var nursingStaff = await _context.NursingStaffs.Where(m => m.EfUserId == user.EfUserId)
                                        .FirstOrDefaultAsync().ConfigureAwait(false);
                                    if (nursingStaff != null)
                                    {
                                        userID = nursingStaff.EfNursingStaffId;
                                    }
                                    break;
                                }
                        }
                        Console.WriteLine("Generating token with personale ID: " + user.PersonaleId + ", User Id: " + userID + " and Role: " + user.Role);
                        var jwt = GenerateToken(user.PersonaleId, userID, user.Role);
                        var token = new Token() { JWT = jwt };
                        return token;
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login");
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Use to change the password.
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPut("Password")]
        public async Task<ActionResult<Token>> ChangePassword([FromBody] Login login)
        {
            if (login == null)
            {
                ModelState.AddModelError(string.Empty, "Data missing");
                return BadRequest(ModelState);
            }

            login.PersonaleId = login.PersonaleId.ToLowerInvariant();
            var user = await _context.Users.Where(u => u.PersonaleId == login.PersonaleId)
                .FirstOrDefaultAsync().ConfigureAwait(false);

            if (user == null)
            {
                ModelState.AddModelError("email", "Not found!");
                return BadRequest(ModelState);
            }

            var validPwd = Verify(login.OldPassword, user.PwHash);
            if (validPwd)
            {
                user.PwHash = HashPassword(login.Password, _appSettings.BcryptWorkfactor);
                await _context.SaveChangesAsync().ConfigureAwait(false);
                return Ok();
            }
            else
            {
                ModelState.AddModelError("oldPassword", "No match");
                return BadRequest(ModelState);
            }
        }

        private string GenerateToken(string personaleId, long userId, Role role)
        {
            Claim roleClaim = role switch
            {
                Role.Admin => new Claim(ClaimTypes.Role, "Admin"),
                Role.Coordinator => new Claim(ClaimTypes.Role, "Coordinator"),
                _ => new Claim(ClaimTypes.Role, "NursingStaff")
            };

            var claims = new Claim[]
            {
                    new Claim(ClaimTypes.Name, personaleId),
                    roleClaim,
                    new Claim("RoleClearText", role.ToString()),
                    new Claim("UserId", userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                    new Claim(JwtRegisteredClaimNames.Exp,
                        new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)),
                new JwtPayload(claims));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
