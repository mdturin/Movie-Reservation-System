using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movi.Core.Domain.Entities;

namespace Movi.WebAPI.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Authorize(Roles = "Admin")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AdminController(UserManager<ApplicationUser> userManager) : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;

    [HttpPost("assign-role")]
    public async Task<IActionResult> AssignRole(string email, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return NotFound("User not found.");

        var result = await _userManager.AddToRoleAsync(user, role);
        if (result.Succeeded)
            return Ok("Role assigned successfully.");

        return BadRequest(result.Errors);
    }
}
