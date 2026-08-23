using MediatR;

namespace Iglesia.Application.Commands.Members.DeactivateMember;

public class DeactivateMemberCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}