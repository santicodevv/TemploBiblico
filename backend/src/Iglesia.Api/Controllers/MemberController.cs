using Iglesia.Application.Members;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Iglesia.Api.Controllers;

[ApiController]
[Route("api/members")]
public class MemberController : ControllerBase
{
    private readonly IMediator _mediator;

    public MemberController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(
            new GetMembersQuery());

        return Ok(result);
    }
}
