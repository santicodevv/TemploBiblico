using Iglesia.Application.FollowUps.Repositories;
using Iglesia.Domain.Entities;
using Iglesia.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Iglesia.Infrastructure.Repositories;

public class FollowUpRepository : IFollowUpRepository
{
    private readonly IglesiaDbContext _context;

    public FollowUpRepository(IglesiaDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(FollowUp followUp, CancellationToken cancellationToken)
    {
        await _context.FollowUps.AddAsync(followUp, cancellationToken);
    }

    public async Task<FollowUp?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.FollowUps
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<FollowUp>> GetByMemberIdAsync(
        Guid memberId,
        CancellationToken cancellationToken)
    {
        return await _context.FollowUps
            .Where(x => x.MemberId == memberId)
            .OrderByDescending(x => x.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<FollowUp>> GetPendingAsync(
        CancellationToken cancellationToken)
    {
        return await _context.FollowUps
            .Where(x => x.NextVisitDate.HasValue)
            .OrderBy(x => x.NextVisitDate)
            .ToListAsync(cancellationToken);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}