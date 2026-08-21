using MediatR;

namespace Iglesia.Application.FollowUps;

public record GetPendingFollowUpsQuery
    : IRequest<IEnumerable<FollowUpDto>>;