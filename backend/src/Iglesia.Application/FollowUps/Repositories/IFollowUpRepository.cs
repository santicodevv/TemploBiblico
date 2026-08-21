using Iglesia.Domain.Entities;

namespace Iglesia.Application.FollowUps.Repositories;

public interface IFollowUpRepository
{
    Task AddAsync(FollowUp followUp, CancellationToken cancellationToken);
    Task<FollowUp?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<FollowUp>> GetByMemberIdAsync(Guid memberId, CancellationToken cancellationToken);
    Task<IEnumerable<FollowUp>> GetPendingAsync(CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}