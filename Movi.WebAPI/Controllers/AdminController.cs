using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Movi.Core.Domain.Abstractions;
using Movi.Core.Domain.Entities;

namespace Movi.WebAPI.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController(UserManager<ApplicationUser> userManager) : AControllerBase
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
