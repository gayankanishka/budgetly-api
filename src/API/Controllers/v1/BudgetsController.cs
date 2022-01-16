using System.Net.Mime;
using Budgetly.Application.Budgets.Commands.CreateBudgetItem;
using Budgetly.Application.Budgets.Commands.DeleteBudgetItem;
using Budgetly.Application.Budgets.Commands.UpdateBudgetItem;
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
public class BudgetsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BudgetsController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet("items")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResponse<BudgetItemDto>>> GetAllAsync(
            [FromQuery] GetBudgetItemsQuery query, CancellationToken cancellationToken)
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
        return new List<BudgetHistoryDto>
        {
            new()
            {
                TargetExpense = 150000,
                ActualExpense = 130000,
                ActualIncome = 250000,
                Date = new DateTime(2021, 9, 1)
            },
            new()
            {
                TargetExpense = 150000,
                ActualExpense = 180000,
                ActualIncome = 200000,
                Date = new DateTime(2021, 10, 1)
            },
            new()
            {
                TargetExpense = 170000,
                ActualExpense = 20000,
                ActualIncome = 340000,
                Date = new DateTime(2021, 11, 1)
            },
            new()
            {
                TargetExpense = 120000,
                ActualExpense = 200000,
                ActualIncome = 200000,
                Date = new DateTime(2021, 12, 1)
            },
            new()
            {
                TargetExpense = 120000,
                ActualExpense = 50000,
                ActualIncome = 100000,
                Date = new DateTime(2022, 01, 1)
            }
        };
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<BudgetItemDto>> CreateAsync([FromBody] CreateBudgetItemCommand command,
    CancellationToken cancellationToken)
    {
        var transaction = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, transaction);
    }

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id,
    [FromBody] UpdateBudgetItemCommand command,
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
        await _mediator.Send(new DeleteBudgetItemCommand(id), cancellationToken);
        return NoContent();
    }
}