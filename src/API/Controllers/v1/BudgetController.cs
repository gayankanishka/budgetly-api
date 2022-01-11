using System.Net.Mime;
using Budgetly.Application.Budgets.Queries.GetBudgets;
using Budgetly.Application.Common.Models;
using Budgetly.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public BudgetController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PagedResponse<BudgetDto>>> GetAllAsync(
            [FromQuery] GetBudgetsQuery query, CancellationToken cancellationToken)
        {
           return await _mediator.Send(query, cancellationToken);
        }
        
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BudgetDto>> GetByIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAsync(CancellationToken cancellationToken)
        {
            // var actionName = nameof(GetByIdAsync);
            // return CreatedAtAction(actionName, new { id = budget.Id }, budget);
            return Ok();
        }
        
        [HttpPut("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return NoContent();
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return NoContent();
        }
    }
}