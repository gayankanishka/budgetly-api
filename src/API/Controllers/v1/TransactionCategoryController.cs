using System.Net.Mime;
using Budgetly.Application.TransactionCategories.Commands.CreateTransactionCategory;
using Budgetly.Application.TransactionCategories.Commands.DeleteTransactionCategory;
using Budgetly.Application.TransactionCategories.Commands.UpdateTransactionCategory;
using Budgetly.Application.TransactionCategories.Queries.GetTransactionCategories;
using Budgetly.Application.TransactionCategories.Queries.GetTransactionCategoryById;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers.v1
{
    [ApiController]
    [Route("api/v1/[controller]")]
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
        
        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionCategoryDto>> GetByIdAsync([FromRoute] GetTransactionCategoryByIdQuery query,
            CancellationToken cancellationToken)
        {
            var transactionCategory = await _mediator.Send(query, cancellationToken);

            if (transactionCategory is null)
            {
                return NotFound();
            }

            return transactionCategory;
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TransactionCategoryDto>> CreateAsync([FromBody] CreatTransactionCategoryCommand command, 
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            var transactionCategory = await _mediator.Send(command, cancellationToken);
            return Created(string.Empty, transactionCategory);
        }
        
        [HttpPut("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateTransactionCategoryCommand command,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var transactionCategory =
                await _mediator.Send(new GetTransactionCategoryByIdQuery(id), cancellationToken);

            if (transactionCategory is null)
            {
                return NotFound();
            }
            
            await _mediator.Send(command, cancellationToken);
            
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteTransactionCategoryCommand command,
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return NoContent();
        }
    }
}