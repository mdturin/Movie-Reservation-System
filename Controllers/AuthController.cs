using Asp.Versioning;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movie_Reservation_System.Data;
using Movie_Reservation_System.Dtos;
using Movie_Reservation_System.Services;
using System.IdentityModel.Tokens.Jwt;

namespace Movie_Reservation_System.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController(UserManager<ApplicationUser> userManager, JwtService jwtService) : ControllerBase
{
    private readonly JwtService _jwtService = jwtService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto registerModel)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (registerModel.Password != registerModel.ConfirmPassword)
            return BadRequest(new { message = "Passwords do not match." });

        var user = new ApplicationUser
        {
            Email = registerModel.Email,
            UserName = registerModel.Username,
        };

        var result = await _userManager
            .CreateAsync(user, registerModel.Password);

        if (result.Succeeded)
        {
            var token = _jwtService.CreateToken(user);
            return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }
}
