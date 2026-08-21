using Iglesia.Domain.Entities;
using MediatR;

namespace Iglesia.Application.Members;

public record GetMembersQuery : IRequest<IEnumerable<Member>>;
