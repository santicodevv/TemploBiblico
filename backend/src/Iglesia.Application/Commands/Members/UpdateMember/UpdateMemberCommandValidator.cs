using FluentValidation;

namespace Iglesia.Application.Commands.Members.UpdateMember;

public class UpdateMemberCommandValidator
    : AbstractValidator<UpdateMemberCommand>
{
    public UpdateMemberCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("El ID del miembro es obligatorio.");

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("El nombre es obligatorio.");

        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("El apellido es obligatorio.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .When(x => !string.IsNullOrEmpty(x.Email))
            .WithMessage("El correo no es válido.");

        RuleFor(x => x.BirthDate)
            .LessThanOrEqualTo(
                DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.BirthDate.HasValue)
            .WithMessage(
                "La fecha de nacimiento no puede ser futura.");

        RuleFor(x => x.ConversionDate)
            .LessThanOrEqualTo(
                DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.ConversionDate.HasValue)
            .WithMessage(
                "La fecha de conversión no puede ser futura.");

        RuleFor(x => x.BaptismDate)
            .LessThanOrEqualTo(
                DateOnly.FromDateTime(DateTime.Today))
            .When(x => x.BaptismDate.HasValue)
            .WithMessage(
                "La fecha de bautismo no puede ser futura.");
    }
}