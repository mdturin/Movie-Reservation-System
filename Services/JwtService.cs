using Microsoft.IdentityModel.Tokens;
using Movie_Reservation_System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Movie_Reservation_System.Services;

public class JwtService(IConfiguration config)
{
    private readonly IConfiguration _config = config;

    public string GenerateToken(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: creds,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:DurationInMinutes"]))
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
