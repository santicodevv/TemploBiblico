using MediatR;
using Microsoft.AspNetCore.Mvc;
using Iglesia.Application.FollowUps;

namespace Iglesia.Api.Controllers;

[ApiController]
[Route("api/followups")]
public class FollowUpController : ControllerBase
{
    private readonly IMediator _mediator;

    public FollowUpController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateFollowUpCommand command)
    {
        var id = await _mediator.Send(command);
        return Ok(id);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateFollowUpCommand command)
    {
        var updatedCommand = command with { Id = id };
        await _mediator.Send(updatedCommand);
        return NoContent();
    }

    [HttpGet("/api/members/{memberId:guid}/followups")]
    public async Task<IActionResult> GetByMember(Guid memberId)
    {
        var result = await _mediator.Send(
            new GetFollowUpsByMemberQuery(memberId));

        return Ok(result);
    }

    [HttpGet("pending")]
    public async Task<IActionResult> GetPending()
    {
        var result = await _mediator.Send(
            new GetPendingFollowUpsQuery());

        return Ok(result);
    }
}