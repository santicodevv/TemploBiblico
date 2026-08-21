using Iglesia.Application.Members.Repositories;
using Iglesia.Domain.Entities;
using Iglesia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Iglesia.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
    private readonly IglesiaDbContext _context;

    public MemberRepository(IglesiaDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Member>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Members
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToListAsync(cancellationToken);
    }

    public async Task<Member?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Members
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task AddAsync(
        Member member,
        CancellationToken cancellationToken)
    {
        await _context.Members.AddAsync(member, cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
