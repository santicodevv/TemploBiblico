using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public DateOnly Date { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public string Organizer { get; private set; } = string.Empty;
    public string? Location { get; private set; }

    private readonly List<Attendance> _attendances = [];
    public IReadOnlyCollection<Attendance> Attendances => _attendances.AsReadOnly();

    private Event() { }

    public Event(
        string title,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        string organizer,
        string? description = null,
        string? location = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Event title is required.");

        if (string.IsNullOrWhiteSpace(organizer))
            throw new DomainException("Event organizer is required.");

        if (endTime <= startTime)
            throw new DomainException("End time must be after start time.");

        Id = Guid.NewGuid();
        Title = title;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Organizer = organizer;
        Description = description;
        Location = location;
    }

    public void Update(
        string title,
        DateOnly date,
        TimeOnly startTime,
        TimeOnly endTime,
        string organizer,
        string? description,
        string? location)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainException("Event title is required.");

        if (string.IsNullOrWhiteSpace(organizer))
            throw new DomainException("Event organizer is required.");

        if (endTime <= startTime)
            throw new DomainException("End time must be after start time.");

        Title = title;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Organizer = organizer;
        Description = description;
        Location = location;
    }
}
