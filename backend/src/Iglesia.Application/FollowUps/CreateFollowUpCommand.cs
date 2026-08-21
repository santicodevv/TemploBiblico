using MediatR;

namespace Iglesia.Application.FollowUps;

public record CreateFollowUpCommand(
    Guid MemberId,
    DateOnly Date,
    string Reason,
    string? Notes,
    string AssignedTo,
    DateOnly? NextVisitDate
) : IRequest<Guid>;