using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Budgetly.API.Controllers
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
        public async Task<IActionResult> GetAsync()
        {
            return Ok();
        }
        
    }
}