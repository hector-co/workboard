using Microsoft.AspNetCore.Mvc;
using MediatR;
using Workboard.Application.Queries.Developers;
using QueryX;

namespace Workboard.WebApi.Controllers;

[Route("workboard/developers")]
public class DevelopersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly QueryBuilder _queryBuilder;

    public DevelopersController(IMediator mediator, QueryBuilder queryBuilder)
    {
        _mediator = mediator;
        _queryBuilder = queryBuilder;
    }

    [HttpGet("{id}", Name = "GetDeveloperById")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var getByIdQuery = new GetDeveloperDtoById(id);
        var result = await _mediator.Send(getByIdQuery, cancellationToken);
        if (result.Data == null) return NotFound();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] QueryModel queryModel, CancellationToken cancellationToken)
    {
        var query = _queryBuilder.CreateQuery<ListDeveloperDto, DeveloperDto>(queryModel);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }
}
