using BudgetManager.Application.Incomes.Commands;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : ApplicationControllerBase
    {
        public IncomeController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddIncomeCommand command, CancellationToken cancellationToken) //From body można pominąć bo jest [APiController]
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }


        [HttpPut("{id}")]
        public IActionResult Edit(Guid id, [FromBody] EditIncomeCommand command, CancellationToken cancellationToken)
        {
            _mediator.Send(command with { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id, CancellationToken cancellationToken)
        {
            _mediator.Send(new DeleteIncomeCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
