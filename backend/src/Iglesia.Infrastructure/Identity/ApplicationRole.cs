using Microsoft.AspNetCore.Identity;

namespace Iglesia.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public string? Descripcion { get; set; }

    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
