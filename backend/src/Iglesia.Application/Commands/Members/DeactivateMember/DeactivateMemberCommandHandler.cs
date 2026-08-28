using Iglesia.Application.Common.Interfaces;
using Iglesia.Domain.Enums;
using MediatR;

namespace Iglesia.Application.Commands.Members.DeactivateMember;

public class DeactivateMemberCommandHandler
    : IRequestHandler<DeactivateMemberCommand, bool>
{
    private readonly IMemberRepository _memberRepository;

    public DeactivateMemberCommandHandler(
        IMemberRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public async Task<bool> Handle(
        DeactivateMemberCommand request,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (member is null)
        {
            return false;
        }

        member.ChangeStatus(MemberStatus.Inactive);

        await _memberRepository.UpdateAsync(
            member,
            cancellationToken);

        await _memberRepository.SaveChangesAsync(
            cancellationToken);

        return true;
    }
}