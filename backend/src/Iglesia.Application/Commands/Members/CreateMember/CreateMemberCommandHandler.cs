using Iglesia.Application.Common.Interfaces;
using Iglesia.Domain.Entities;
using MediatR;

namespace Iglesia.Application.Commands.Members.CreateMember;

public class CreateMemberCommandHandler
    : IRequestHandler<CreateMemberCommand, Guid>
{
    private readonly IMemberRepository _memberRepository;

    public CreateMemberCommandHandler(
        IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<Guid> Handle(
        CreateMemberCommand request,
        CancellationToken cancellationToken)
    {
        var member = new Member(
            request.FirstName,
            request.LastName,
            request.BirthDate,
            request.Phone,
            request.Address,
            request.Email);

        if (request.ConversionDate.HasValue)
        {
            member.RegisterConversion(request.ConversionDate.Value);
        }

        if (request.BaptismDate.HasValue)
        {
            member.RegisterBaptism(request.BaptismDate.Value);
        }

        if (request.MinistryId.HasValue)
        {
            member.AssignMinistry(request.MinistryId.Value);
        }

        await _memberRepository.AddAsync(
            member,
            cancellationToken);

        await _memberRepository.SaveChangesAsync(
            cancellationToken);

        return member.Id;
    }
}