using BudgetManager.Application.ExpenseTables.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("/api/controller")]
    [ApiController]
    public class ExpensesTableController : ApplicationControllerBase
    {
        public ExpensesTableController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetExpenseTableQuery(), cancellationToken));
        }
    }
}
