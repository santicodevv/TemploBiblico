using Iglesia.Application.Common.Interfaces;
using Iglesia.Domain.Entities;
using Iglesia.Domain.Enums;
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

    public async Task<Member?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Members
            .Include(m => m.Ministry)
            .FirstOrDefaultAsync(
                m => m.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Member>> GetAllAsync(
        MemberStatus? status = null,
        Guid? ministryId = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<Member> query = _context.Members
            .Include(m => m.Ministry);

        if (status.HasValue)
        {
            query = query.Where(m => m.Status == status.Value);
        }

        if (ministryId.HasValue)
        {
            query = query.Where(m => m.MinistryId == ministryId.Value);
        }

        return await query
            .OrderBy(m => m.LastName)
            .ThenBy(m => m.FirstName)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        Member member,
        CancellationToken cancellationToken = default)
    {
        await _context.Members.AddAsync(member, cancellationToken);
    }

    public Task UpdateAsync(
        Member member,
        CancellationToken cancellationToken = default)
    {
        _context.Members.Update(member);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}