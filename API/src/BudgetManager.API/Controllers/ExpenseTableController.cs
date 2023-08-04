using BudgetManager.Application.ExpenseTables.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class ExpenseTableController : ApplicationControllerBase
    {
        public ExpenseTableController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get(int Year, int Month, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetExpenseTableQuery(Year, Month), cancellationToken));
        }
    }
}
