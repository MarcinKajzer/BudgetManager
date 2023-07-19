using BudgetManager.Application.IncomesTable.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    public class IncomeTableController : ApplicationControllerBase
    {
        public IncomeTableController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetIncomeTableQuery(), cancellationToken));
        }
    }
}
