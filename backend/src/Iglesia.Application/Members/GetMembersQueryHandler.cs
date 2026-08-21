using Iglesia.Application.Members.Repositories;
using Iglesia.Domain.Entities;
using MediatR;

namespace Iglesia.Application.Members;

public class GetMembersQueryHandler
    : IRequestHandler<GetMembersQuery, IEnumerable<Member>>
{
    private readonly IMemberRepository _repository;

    public GetMembersQueryHandler(IMemberRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Member>> Handle(
        GetMembersQuery request,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
