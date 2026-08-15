using Iglesia.Application.Dtos;

namespace Iglesia.Application.Interfaces;

public interface IAuthService
{
    Task<ResultadoAuth> LoginAsync(LoginRequestDto dto);
    Task<ResultadoAuth> RefreshTokenAsync(RefreshTokenRequestDto dto);
}