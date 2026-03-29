using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AuthAPI.Services;

namespace AuthAPI.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly AuthService _authService;

    public UserController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetProfile()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null || !int.TryParse(userIdClaim, out int userId))
            return Unauthorized(new { message = "Invalid token." });

        var user = await _authService.GetUserByIdAsync(userId);
        if (user == null)
            return NotFound(new { message = "User not found." });

        return Ok(new
        {
            id = user.Id,
            name = user.Name,
            email = user.Email,
            createdAt = user.CreatedAt
        });
    }
}
