using FluentValidation;

namespace Iglesia.Application.Queries.Members;

public class GetMemberByIdQueryValidator
    : AbstractValidator<GetMemberByIdQuery>
{
    public GetMemberByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Member ID is required.");
    }
}