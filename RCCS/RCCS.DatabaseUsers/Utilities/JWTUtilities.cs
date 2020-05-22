using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace RCCS.DatabaseUsers.Utilities
{
    public static class JWTUtilities
    {
        ////Skal tilpasses
        //public static string GenerateToken(string email, long modelId, bool isManager)
        //{
        //    Claim roleClaim;
        //    if (isManager)
        //        roleClaim = new Claim("Role", "Admin");
        //    else
        //        roleClaim = new Claim("Role", "Coordinator");

        //    var claims = new Claim[]
        //    {
        //        new Claim("Email", email),
        //        roleClaim,
        //        new Claim("ModelId", modelId.ToString()),
        //        new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
        //        new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
        //    };

        //    var token = new JwtSecurityToken(
        //         new JwtHeader(new SigningCredentials(
        //              new SymmetricSecurityKey(Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256")),
        //                                       SecurityAlgorithms.HmacSha256)),
        //              new JwtPayload(claims));

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

    }
}
