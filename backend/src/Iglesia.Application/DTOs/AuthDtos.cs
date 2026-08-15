
namespace Iglesia.Aplication.Dtos;

public record LoginRequestDto(
    string Email,
    string Password
);

public record RefreshTokenRequestDto (
    string token,
    string RefreshToken
);

public record AuthResponseDto(
    string token,
    string RefreshToken,
    string Email,
    string Rol,
    int? MinisterioId
);

public class ResultadoAuth
{
    public bool Exitoso { get; set; }
    public string? Error { get; set; }
    public AuthResponseDto? TokenResponse { get; set; }

    public static ResultadoAuth Ok(AuthResponseDto response) => 
        new() { Exitoso = true, TokenResponse = response };

    public static ResultadoAuth Fallo(string mensaje) => 
        new() { Exitoso = false, Error = mensaje };
}