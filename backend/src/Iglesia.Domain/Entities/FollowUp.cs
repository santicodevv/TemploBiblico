using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

public class FollowUp
{
    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public DateOnly Date { get; private set; }
    public string Reason { get; private set; } = string.Empty;
    public string? Notes { get; private set; }
    public DateOnly? NextVisitDate { get; private set; }
    public string AssignedTo { get; private set; } = string.Empty;

    public Member Member { get; private set; } = null!;

    private FollowUp() { }

    public FollowUp(
        Guid memberId,
        DateOnly date,
        string reason,
        string assignedTo,
        string? notes = null,
        DateOnly? nextVisitDate = null)
    {
        if (memberId == Guid.Empty)
            throw new DomainException("Member is required for follow-up.");

        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Follow-up reason is required.");

        if (string.IsNullOrWhiteSpace(assignedTo))
            throw new DomainException("Assigned person is required.");

        if (nextVisitDate.HasValue &&
            nextVisitDate.Value <= DateOnly.FromDateTime(DateTime.Today))
            throw new DomainException("Next visit date must be in the future.");

        Id = Guid.NewGuid();
        MemberId = memberId;
        Date = date;
        Reason = reason;
        AssignedTo = assignedTo;
        Notes = notes;
        NextVisitDate = nextVisitDate;
    }

    public void Update(
        string reason,
        string assignedTo,
        string? notes,
        DateOnly? nextVisitDate)
    {
        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Follow-up reason is required.");

        if (string.IsNullOrWhiteSpace(assignedTo))
            throw new DomainException("Assigned person is required.");

        if (nextVisitDate.HasValue &&
            nextVisitDate.Value <= DateOnly.FromDateTime(DateTime.Today))
            throw new DomainException("Next visit date must be in the future.");

        Reason = reason;
        AssignedTo = assignedTo;
        Notes = notes;
        NextVisitDate = nextVisitDate;
    }
}
