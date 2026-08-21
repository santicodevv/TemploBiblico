using Iglesia.Application.FollowUps.Repositories;
using Iglesia.Domain.Entities;
using MediatR;

namespace Iglesia.Application.FollowUps;

public class CreateFollowUpCommandHandler
    : IRequestHandler<CreateFollowUpCommand, Guid>
{
    private readonly IFollowUpRepository _repository;

    public CreateFollowUpCommandHandler(IFollowUpRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CreateFollowUpCommand request,
        CancellationToken cancellationToken)
    {
        var followUp = new FollowUp(
            request.MemberId,
            request.Date,
            request.Reason,
            request.AssignedTo,
            request.Notes,
            request.NextVisitDate);

        await _repository.AddAsync(followUp, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return followUp.Id;
    }
}