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

    public DbSet<Member> Members => Set<Member>();
    public DbSet<Ministry> Ministries => Set<Ministry>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    public DbSet<FollowUp> FollowUps => Set<FollowUp>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(IglesiaDbContext).Assembly);
    }
}
