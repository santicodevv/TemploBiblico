using Microsoft.AspNetCore.Identity;

namespace Iglesia.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string Nombres { get; set; } = string.Empty;
    public string Apellidos { get; set; } = string.Empty;
    public bool Activo { get; set; } = true;
    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}
