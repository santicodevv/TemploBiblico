using Iglesia.Application.DTOs.Members;
using MediatR;

namespace Iglesia.Application.Queries.Members;

public record GetMemberByIdQuery(Guid Id) : IRequest<MemberDto>;