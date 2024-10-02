using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Dtos;
using Movi.Core.Domain.Entities;
using Movi.Core.Domain.Interfaces;

namespace Movi.WebAPI.Controllers;

public class AuthController(
    IJwtService jwtService,
    IUserRepository userRepository,
    UserManager<ApplicationUser> userManager
    ) : AControllerBase
{
    private readonly IJwtService _jwtService = jwtService;
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IUserRepository _userRepository = userRepository;

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
            await _userManager.AddToRoleAsync(user, "User");
            return Ok(new { token = await _jwtService.GenerateTokenAsync(user) });
        }

        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }

        return BadRequest(ModelState);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginModel)
    {
        var user = await _userRepository
            .ValidateUserAsync(loginModel.Username, loginModel.Password);
        if (user == null)
            return Unauthorized("Invalid username or password");
        return Ok(new { Token = await _jwtService.GenerateTokenAsync(user) });
    }
}
