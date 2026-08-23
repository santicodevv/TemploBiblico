using FluentValidation;

namespace Iglesia.Application.Commands.Members.DeactivateMember;

public class DeactivateMemberCommandValidator
    : AbstractValidator<DeactivateMemberCommand>
{
    public DeactivateMemberCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El ID del miembro es obligatorio.");
    }
}