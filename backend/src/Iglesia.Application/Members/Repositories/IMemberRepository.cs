using Iglesia.Domain.Entities;

namespace Iglesia.Application.Members.Repositories;

public interface IMemberRepository
{
    Task<IEnumerable<Member>> GetAllAsync(CancellationToken cancellationToken);

    Task<Member?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task AddAsync(
        Member member,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}
