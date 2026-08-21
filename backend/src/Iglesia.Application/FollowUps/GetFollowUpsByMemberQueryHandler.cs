using Iglesia.Application.FollowUps.Repositories;
using MediatR;

namespace Iglesia.Application.FollowUps;

public class GetFollowUpsByMemberQueryHandler
    : IRequestHandler<GetFollowUpsByMemberQuery, IEnumerable<FollowUpDto>>
{
    private readonly IFollowUpRepository _repository;

    public GetFollowUpsByMemberQueryHandler(IFollowUpRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FollowUpDto>> Handle(
        GetFollowUpsByMemberQuery request,
        CancellationToken cancellationToken)
    {
        var followUps = await _repository.GetByMemberIdAsync(
            request.MemberId,
            cancellationToken);

        return followUps.Select(f => new FollowUpDto(
            f.Id,
            f.MemberId,
            f.Date,
            f.Reason,
            f.Notes,
            f.AssignedTo,
            f.NextVisitDate));
    }
}