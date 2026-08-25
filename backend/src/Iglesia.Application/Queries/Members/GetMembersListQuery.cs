using Iglesia.Application.DTOs.Members;
using Iglesia.Domain.Enums;
using MediatR;

namespace Iglesia.Application.Queries.Members;

public class GetMembersListQuery : IRequest<IReadOnlyList<MemberDto>>
{
    public MemberStatus? Status { get; }

    public Guid? MinistryId { get; }

    public GetMembersListQuery(
        MemberStatus? status = null,
        Guid? ministryId = null)
    {
        Status = status;
        MinistryId = ministryId;
    }
}