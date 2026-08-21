using Iglesia.Application.FollowUps.Repositories;
using MediatR;

namespace Iglesia.Application.FollowUps;

public class UpdateFollowUpCommandHandler
    : IRequestHandler<UpdateFollowUpCommand>
{
    private readonly IFollowUpRepository _repository;

    public UpdateFollowUpCommandHandler(IFollowUpRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        UpdateFollowUpCommand request,
        CancellationToken cancellationToken)
    {
        var followUp = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (followUp is null)
            return;

        followUp.Update(
            followUp.Reason,
            followUp.AssignedTo,
            request.Notes,
            request.NextVisitDate);

        await _repository.SaveChangesAsync(cancellationToken);
    }
}