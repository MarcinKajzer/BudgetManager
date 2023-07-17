using BudgetManager.Application.IncomeCategories.Commands;
using BudgetManager.Application.IncomeCategories.Queries;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeCategoryController : ApplicationControllerBase
    {
        public IncomeCategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddIncomeCategoryCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return CreatedAtAction(nameof(Get), new { id }, null);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetIncomeCategoryQuery(id), cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllIncomeCategoriesQuery(), cancellationToken));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] EditIncomeCategoryCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command with { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteIncomeCategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
