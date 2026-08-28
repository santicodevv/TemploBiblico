using AutoMapper;
using Iglesia.Application.Common.Interfaces;
using Iglesia.Application.DTOs.Members;
using MediatR;

namespace Iglesia.Application.Queries.Members;

public class GetMembersListQueryHandler
    : IRequestHandler<GetMembersListQuery, IReadOnlyList<MemberDto>>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetMembersListQueryHandler(
        IMemberRepository memberRepository,
        IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<IReadOnlyList<MemberDto>> Handle(
        GetMembersListQuery request,
        CancellationToken cancellationToken)
    {
        var members = await _memberRepository.GetAllAsync(
            request.Status,
            request.MinistryId,
            cancellationToken);

        return _mapper.Map<IReadOnlyList<MemberDto>>(members);
    }
}