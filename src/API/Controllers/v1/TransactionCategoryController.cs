using System.Net.Mime;
using Budgetly.Application.Common.Models;
using Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;
using Budgetly.Application.TransactionCategories.Commands.DeleteTransactionCategory;
using Budgetly.Application.TransactionCategories.Commands.UpdateTransactionCategory;
using Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;
using Budgetly.Application.TransactionCategories.Queries.GetTransactionCategoryById;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers.v1;

[ApiController]
[Produces(MediaTypeNames.Application.Json)]
[Route("api/v1/[controller]")]
[Authorize]
public class TransactionCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public TransactionCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PagedResponse<TransactionCategoryDto>>> GetAllAsync(
        [FromQuery] GetTransactionCategoriesQuery query, CancellationToken cancellationToken)
    {
        return await _mediator.Send(query, cancellationToken);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TransactionCategoryDto>> GetByIdAsync([FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return await _mediator.Send(new GetTransactionCategoryByIdQuery(id), cancellationToken);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TransactionCategoryDto>> CreateAsync(
        [FromBody] CreatTransactionCategoryCommand command,
        CancellationToken cancellationToken)
    {
        var transactionCategory = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, transactionCategory);
    }

    [HttpPut("{id:int}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id,
        [FromBody] UpdateTransactionCategoryCommand command,
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
        await _mediator.Send(new DeleteTransactionCategoryCommand(id), cancellationToken);
        return NoContent();
    }
}