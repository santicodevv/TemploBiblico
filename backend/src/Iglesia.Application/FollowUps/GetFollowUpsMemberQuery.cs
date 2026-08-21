using MediatR;

namespace Iglesia.Application.FollowUps;

public record GetFollowUpsByMemberQuery(
    Guid MemberId
) : IRequest<IEnumerable<FollowUpDto>>;