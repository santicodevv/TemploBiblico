namespace Iglesia.Application.FollowUps;

public record FollowUpDto(
    Guid Id,
    Guid MemberId,
    DateOnly Date,
    string Reason,
    string? Notes,
    string AssignedTo,
    DateOnly? NextVisitDate
);