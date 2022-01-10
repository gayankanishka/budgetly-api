using System.Net.Mime;
using Budgetly.Application.Transactions.Commands.CreateTransaction;
using Budgetly.Application.Transactions.Commands.DeleteTransaction;
using Budgetly.Application.Transactions.Commands.UpdateTransaction;
using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Dtos;
using Budgetly.Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
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
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TransactionDto>> CreateAsync([FromBody] CreateTransactionCommand command,
            CancellationToken cancellationToken)
        {
            return await _mediator.Send(command, cancellationToken);
        }
        
        [HttpPut("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteTransactionCommand { Id = id }, cancellationToken);
            return NoContent();
        }
    }
}