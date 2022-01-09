using Budgetly.Application.Responses;
using Budgetly.Application.Transactions.Queries.GetTransactions;
using Budgetly.Domain.Common;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        // TODO: GW | Required filters
        // search by name, date filter with start and end, by recurring state

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
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken)
        {
            return Ok();
        }
        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }
        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return NoContent();
        }
    }
}