using Budgetly.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers
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
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            return Ok();
        }
        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }
        
        [HttpGet("category/{id:int}/transactions")]
        public async Task<IActionResult> GetTransactionsByCategoryIdAsync([FromRoute] int id,
            CancellationToken cancellationToken)
        {
            return Ok();
        }
        
        [HttpGet("recurring")]
        public async Task<IActionResult> GetRecurringTransactionsAsync(CancellationToken cancellationToken)
        {
            return Ok();
        }
        
        [HttpGet("type/{type}/transactions")]
        public async Task<IActionResult> GetTransactionsByTypeAsync([FromRoute] TransactionTypes type,
            CancellationToken cancellationToken)
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