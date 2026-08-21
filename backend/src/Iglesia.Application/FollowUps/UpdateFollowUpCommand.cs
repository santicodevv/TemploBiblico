using MediatR;

namespace Iglesia.Application.FollowUps;

public record UpdateFollowUpCommand(
    Guid Id,
    string? Notes,
    DateOnly? NextVisitDate
) : IRequest;