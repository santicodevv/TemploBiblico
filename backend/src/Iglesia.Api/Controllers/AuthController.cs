using Iglesia.Application.Dtos;
using Iglesia.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        var resultado = await _authService.LoginAsync(dto);

        if (!resultado.Exitoso)
        {
            return BadRequest(new { mensaje = resultado.Error });
        }

        return Ok(resultado.TokenResponse);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
    {
        var resultado = await _authService.RefreshTokenAsync(dto);

        if (!resultado.Exitoso)
        {
            return BadRequest(new { mensaje = resultado.Error });
        }

        return Ok(resultado.TokenResponse);
    }
}