using MediatR;

namespace Iglesia.Application.Commands.Members.UpdateMember;

public class UpdateMemberCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = "";

    public string LastName { get; set; } = "";

    public DateOnly? BirthDate { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? Email { get; set; }

    public DateOnly? ConversionDate { get; set; }

    public DateOnly? BaptismDate { get; set; }

    public Guid? MinistryId { get; set; }
}