using Iglesia.Application.Commands.Members.UpdateMember;
using Iglesia.Application.Commands.Members.DeactivateMember;
using Iglesia.Application.Commands.Members.CreateMember;
using Iglesia.Application.DTOs.Members;
using Iglesia.Application.Queries.Members;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Iglesia.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly ISender _sender;

    public MembersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateMemberCommand command,
        CancellationToken cancellationToken)
    {
        var memberId = await _sender.Send(
            command,
            cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = memberId },
            memberId);
    }

    [HttpPut("{id:guid}")]
public async Task<IActionResult> Update(
    Guid id,
    [FromBody] UpdateMemberCommand command,
    CancellationToken cancellationToken)
{
    command.Id = id;

    var updated = await _sender.Send(
        command,
        cancellationToken);

    if (!updated)
    {
        return NotFound();
    }

    return NoContent();
}

[HttpDelete("{id:guid}")]
public async Task<IActionResult> Deactivate(
    Guid id,
    CancellationToken cancellationToken)
{
    var deactivated = await _sender.Send(
        new DeactivateMemberCommand
        {
            Id = id
        },
        cancellationToken);

    if (!deactivated)
    {
        return NotFound();
    }

    return NoContent();
}

    [HttpGet]
    public async Task<ActionResult<List<MemberDto>>> GetAll(
        CancellationToken cancellationToken)
    {
        var members = await _sender.Send(
            new GetMembersQuery(),
            cancellationToken);

        return Ok(members);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MemberDto>> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var member = await _sender.Send(
            new GetMemberByIdQuery(id),
            cancellationToken);

        return Ok(member);
    }
}