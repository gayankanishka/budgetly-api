using System.Net.Mime;
using Budgetly.Application.Budgets.Commands.CreateBudget;
using Budgetly.Application.Budgets.Commands.DeleteBudget;
using Budgetly.Application.Budgets.Commands.UpdateBudget;
using Budgetly.Application.Budgets.Queries.GetBudgetHistory;
using Budgetly.Application.Budgets.Queries.GetBudgets;
using Budgetly.Application.Budgets.Queries.GetCurrentBudgetStat;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers.v1;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/[controller]")]
[Authorize]
public class BudgetController : ControllerBase
{
    private readonly IMediator _mediator;

    public BudgetController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResponse<BudgetDto>>> GetAllAsync(
        [FromQuery] GetBudgetsQuery query, CancellationToken cancellationToken)
    {
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpGet("current-stat")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BudgetStatDto>> GetCurrentBudgetStatAsync(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetCurrentBudgetStatQuery(), cancellationToken);
    }

    [HttpGet("history")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IEnumerable<BudgetHistoryDto>> GetBudgetHistoryAsync(CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetBudgetHistoryQuery(), cancellationToken);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<int>> CreateAsync([FromBody] CreateBudgetCommand command,
        CancellationToken cancellationToken)
    {
        var budgetId = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, budgetId);
    }

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id,
        [FromBody] UpdateBudgetCommand command,
        CancellationToken cancellationToken)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }

        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBudgetCommand(id), cancellationToken);
        return NoContent();
    }
}