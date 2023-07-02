using BudgetManager.Application.Expenses.Commands;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesControler : ApplicationControllerBase
    {
        public ExpensesControler(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddExpenseCommand command, CancellationToken cancellationToken) //From body można pominąć bo jest [APiController]
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }


        [HttpPut("{id}")]
        public IActionResult Edit(Guid id, [FromBody] EditExpenseCommand command, CancellationToken cancellationToken)
        {
            _mediator.Send(command with { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id, CancellationToken cancellationToken)
        {
            _mediator.Send(new DeleteExpenseCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
