using Microsoft.AspNetCore.Mvc;
using MediatR;
using Workboard.Application.Queries.Boards;
using Workboard.Application.Commands.Boards;
using QueryX;
using Workboard.Application.Queries.BoardItems;

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

    [HttpGet("{id}/items")]
    public async Task<IActionResult> ListBoardItems(int id, [FromQuery] QueryModel queryModel, CancellationToken cancellationToken)
    {
        var query = _queryBuilder.CreateQuery<ListBoardItemDto, BoardItemDto>(queryModel);
        query.BoardId = id;
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost("{id}/items")]
    public async Task<IActionResult> AddBoardItem(int id, [FromBody] AddBoardItem command, CancellationToken cancellationToken)
    {
        command.BoardId = id;
        await _mediator.Send(command, cancellationToken);
        return Accepted();
    }

    [HttpPost("{id}/items/{itemId}/move-to")]
    public async Task<IActionResult> MoveItem(int id, int itemId, [FromBody] MoveBoardItem command, CancellationToken cancellationToken)
    {
        command.BoardId = id;
        command.BoardItemId = itemId;
        await _mediator.Send(command, cancellationToken);
        return Accepted();
    }
}
