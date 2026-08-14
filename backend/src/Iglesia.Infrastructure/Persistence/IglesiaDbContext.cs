using Iglesia.Domain.Entities;
using Iglesia.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Iglesia.Infrastructure.Persistence;

public class IglesiaDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public IglesiaDbContext(DbContextOptions<IglesiaDbContext> options) : base(options)
    {
    }

    public DbSet<Miembro> Miembros => Set<Miembro>();
    public DbSet<Ministerio> Ministerios => Set<Ministerio>();
    public DbSet<Evento> Eventos => Set<Evento>();
    public DbSet<Asistencia> Asistencias => Set<Asistencia>();
    public DbSet<Seguimiento> Seguimientos => Set<Seguimiento>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Apply all configurations from this assembly
        builder.ApplyConfigurationsFromAssembly(typeof(IglesiaDbContext).Assembly);
    }
}
