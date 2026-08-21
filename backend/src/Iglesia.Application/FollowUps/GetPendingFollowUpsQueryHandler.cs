using Iglesia.Application.FollowUps.Repositories;
using MediatR;

namespace Iglesia.Application.FollowUps;

public class GetPendingFollowUpsQueryHandler
    : IRequestHandler<GetPendingFollowUpsQuery, IEnumerable<FollowUpDto>>
{
    private readonly IFollowUpRepository _repository;

    public GetPendingFollowUpsQueryHandler(IFollowUpRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<FollowUpDto>> Handle(
        GetPendingFollowUpsQuery request,
        CancellationToken cancellationToken)
    {
        var followUps = await _repository.GetPendingAsync(cancellationToken);

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