using Microsoft.AspNetCore.Mvc;
using MediatR;
using Workboard.Application.Queries.Boards;
using Workboard.Application.Commands.Boards;
using QueryX;

namespace Workboard.WebApi.Controllers;

[Route("workboard/boards")]
public class BoardsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly QueryBuilder _queryBuilder;

    public BoardsController(IMediator mediator, QueryBuilder queryBuilder)
    {
        _mediator = mediator;
        _queryBuilder = queryBuilder;
    }

    [HttpGet("{id}", Name = "GetBoardById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var getByIdQuery = new GetBoardDtoById(id);
        var result = await _mediator.Send(getByIdQuery, cancellationToken);
        if (result.Data == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] QueryModel queryModel, CancellationToken cancellationToken)
    {
        var query = _queryBuilder.CreateQuery<ListBoardDto, BoardDto>(queryModel);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterBoard command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        response.Verify();
        var model = await _mediator.Send(new GetBoardDtoById(response.Value), cancellationToken);
        return CreatedAtRoute("GetBoardById", new { id = response.Value }, model);
    }
}
