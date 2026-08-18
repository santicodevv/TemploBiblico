namespace Iglesia.Application.Dtos;

public record LoginRequestDto(
    string Email,
    string Password
);

public record RefreshTokenRequestDto(
    string Token,
    string RefreshToken
);

public record AuthResponseDto(
    string Token,
    string RefreshToken,
    DateTime Expiracion,
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