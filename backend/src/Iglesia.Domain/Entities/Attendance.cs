using Iglesia.Domain.Enums;
using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

public class Attendance
{
    public Guid Id { get; private set; }
    public Guid MemberId { get; private set; }
    public Guid EventId { get; private set; }
    public DateOnly Date { get; private set; }
    public AttendanceStatus Status { get; private set; }
    public string? Notes { get; private set; }

    public Member Member { get; private set; } = null!;
    public Event Event { get; private set; } = null!;

    private Attendance() { }

    public Attendance(
        Guid memberId,
        Guid eventId,
        DateOnly date,
        AttendanceStatus status,
        string? notes = null)
    {
        if (memberId == Guid.Empty)
            throw new DomainException("Member is required for attendance.");

        if (eventId == Guid.Empty)
            throw new DomainException("Event is required for attendance.");

        Id = Guid.NewGuid();
        MemberId = memberId;
        EventId = eventId;
        Date = date;
        Status = status;
        Notes = notes;
    }

    public void ChangeStatus(AttendanceStatus status, string? notes = null)
    {
        Status = status;
        Notes = notes;
    }
}
