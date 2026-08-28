using AutoMapper;
using Iglesia.Application.Common.Interfaces;
using Iglesia.Application.DTOs.Members;
using MediatR;

namespace Iglesia.Application.Queries.Members;

public class GetMemberByIdQueryHandler
    : IRequestHandler<GetMemberByIdQuery, MemberDto>
{
    private readonly IMemberRepository _memberRepository;
    private readonly IMapper _mapper;

    public GetMemberByIdQueryHandler(
        IMemberRepository memberRepository,
        IMapper mapper)
    {
        _memberRepository = memberRepository;
        _mapper = mapper;
    }

    public async Task<MemberDto> Handle(
        GetMemberByIdQuery request,
        CancellationToken cancellationToken)
    {
        var member = await _memberRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (member is null)
            throw new KeyNotFoundException(
                $"Member with ID '{request.Id}' was not found.");

        return _mapper.Map<MemberDto>(member);
    }
}