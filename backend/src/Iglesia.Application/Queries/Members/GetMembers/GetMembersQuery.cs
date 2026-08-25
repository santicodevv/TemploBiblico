using Iglesia.Application.DTOs.Members;
using MediatR;

namespace Iglesia.Application.Queries.Members;

public record GetMembersQuery : IRequest<List<MemberDto>>;