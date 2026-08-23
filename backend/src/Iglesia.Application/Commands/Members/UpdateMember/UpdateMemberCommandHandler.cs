using Iglesia.Application.Common.Interfaces;
using MediatR;

namespace Iglesia.Application.Commands.Members.UpdateMember;

public class UpdateMemberCommandHandler
    : IRequestHandler<UpdateMemberCommand, bool>
{
    private readonly IMemberRepository _memberRepository;

    public UpdateMemberCommandHandler(
        IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<bool> Handle(
        UpdateMemberCommand request,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (member is null)
        {
            return false;
        }

        member.UpdatePersonalInfo(
            request.FirstName,
            request.LastName,
            request.BirthDate,
            request.Phone,
            request.Address,
            request.Email);

        if (request.ConversionDate.HasValue)
        {
            member.RegisterConversion(
                request.ConversionDate.Value);
        }

        if (request.BaptismDate.HasValue)
        {
            member.RegisterBaptism(
                request.BaptismDate.Value);
        }

        member.AssignMinistry(request.MinistryId);

        await _memberRepository.UpdateAsync(
            member,
            cancellationToken);

        await _memberRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}