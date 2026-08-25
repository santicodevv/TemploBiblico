namespace Iglesia.Application.DTOs.Members;

public class UpdateMemberDto
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly? BirthDate { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public DateOnly? ConversionDate { get; set; }

    public DateOnly? BaptismDate { get; set; }

    public Guid? MinistryId { get; set; }
}