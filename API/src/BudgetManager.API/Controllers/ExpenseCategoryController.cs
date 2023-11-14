using BudgetManager.Application.Categories.Commands;
using BudgetManager.Application.Categories.Queries;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseCategoryController : ApplicationControllerBase
    {
        public ExpenseCategoryController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Add(AddExpenseCategoryCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetExpenseCategoryQuery(id), cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllExpenseCategoriesQuery(), cancellationToken));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, EditExpenseCategoryCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command with { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteExpenseCategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
