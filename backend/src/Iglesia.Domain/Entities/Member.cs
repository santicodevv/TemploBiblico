using Iglesia.Domain.Enums;
using Iglesia.Domain.Exceptions;

namespace Iglesia.Domain.Entities;

/// <summary>
/// Represents a church member.
/// </summary>
/// <remarks>
/// TODO: Currently a member can only belong to ONE ministry (1-N relationship).
/// If the team decides that a member can belong to multiple ministries,
/// a MemberMinistry junction table (N-N relationship) must be created.
/// Discuss with the team before implementing changes.
/// </remarks>
public class Member
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public DateOnly? BirthDate { get; private set; }
    public string? Phone { get; private set; }
    public string? Address { get; private set; }
    public string? Email { get; private set; }
    public DateOnly? ConversionDate { get; private set; }
    public DateOnly? BaptismDate { get; private set; }
    public MemberStatus Status { get; private set; }
    public string? PhotoUrl { get; private set; }

    public Guid? MinistryId { get; private set; }
    public Ministry? Ministry { get; private set; }

    private readonly List<Attendance> _attendances = [];
    public IReadOnlyCollection<Attendance> Attendances => _attendances.AsReadOnly();

    private readonly List<FollowUp> _followUps = [];
    public IReadOnlyCollection<FollowUp> FollowUps => _followUps.AsReadOnly();

    private Member() { }

    public Member(
        string firstName,
        string lastName,
        DateOnly? birthDate = null,
        string? phone = null,
        string? address = null,
        string? email = null)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name is required.");

        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Phone = phone;
        Address = address;
        Email = email;
        Status = MemberStatus.Active;
    }

    public void UpdatePersonalInfo(
        string firstName,
        string lastName,
        DateOnly? birthDate,
        string? phone,
        string? address,
        string? email)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new DomainException("First name is required.");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new DomainException("Last name is required.");

        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Phone = phone;
        Address = address;
        Email = email;
    }

    public void RegisterConversion(DateOnly date)
    {
        ConversionDate = date;
    }

    public void RegisterBaptism(DateOnly date)
    {
        if (ConversionDate.HasValue && date < ConversionDate.Value)
            throw new DomainException("Baptism date cannot be before conversion date.");

        BaptismDate = date;
    }

    public void AssignMinistry(Guid? ministryId)
    {
        MinistryId = ministryId;
    }

    public void ChangeStatus(MemberStatus status)
    {
        Status = status;
    }

    public void UpdatePhoto(string? photoUrl)
    {
        PhotoUrl = photoUrl;
    }
}
