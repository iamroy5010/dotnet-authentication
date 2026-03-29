using Microsoft.AspNetCore.Mvc;
using AuthAPI.DTOs;
using AuthAPI.Services;

namespace AuthAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, message) = await _authService.RegisterAsync(dto);
        if (!success)
            return BadRequest(new { message });

        return Ok(new { message });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var (success, token, message) = await _authService.LoginAsync(dto);
        if (!success)
            return Unauthorized(new { message });

        return Ok(new { token, message });
    }
}
