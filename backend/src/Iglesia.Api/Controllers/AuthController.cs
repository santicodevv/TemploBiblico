using Microsoft.AspNetCore.Mvc;
using Iglesia.Application.Interfaces;
using Iglesia.Application.DTOs;

namespace Iglesia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        if (!result.Exitoso)
            return Unauthorized(new { mensaje = result.Error });

        return Ok(result.TokenResponse);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDto dto)
    {
        var result = await _authService.RefreshTokenAsync(dto);
        if (!result.Exitoso)
            return BadRequest(new { mensaje = result.Error });

        return Ok(result.TokenResponse);
    }
}