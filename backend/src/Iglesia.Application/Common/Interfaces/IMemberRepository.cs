using Iglesia.Domain.Entities;
using Iglesia.Domain.Enums;

namespace Iglesia.Application.Common.Interfaces;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Member>> GetAllAsync(
        MemberStatus? status = null,
        Guid? ministryId = null,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        Member member,
        CancellationToken cancellationToken = default);

    Task UpdateAsync(
        Member member,
        CancellationToken cancellationToken = default);

    Task SaveChangesAsync(
        CancellationToken cancellationToken = default);
}