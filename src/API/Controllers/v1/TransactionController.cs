using System.Net.Mime;
using Budgetly.Application.Common.Models;
using Budgetly.Application.Transactions.Commands.CreateTransaction;
using Budgetly.Application.Transactions.Commands.DeleteTransaction;
using Budgetly.Application.Transactions.Commands.UpdateTransaction;
using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers.v1;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/[controller]")]
[Authorize]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResponse<TransactionDto>>> GetAllAsync(
        [FromQuery] GetTransactionsQuery query, CancellationToken cancellationToken)
    {
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<int>> CreateAsync([FromBody] CreateTransactionCommand command,
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
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateTransactionCommand command,
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
        await _mediator.Send(new DeleteTransactionCommand(id), cancellationToken);
        return NoContent();
    }
}