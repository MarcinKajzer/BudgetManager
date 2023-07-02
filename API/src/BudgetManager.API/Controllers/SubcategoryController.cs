using BudgetManager.Application.Subcategories.Commands;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubcategoryController : ApplicationControllerBase
    {
        public SubcategoryController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddSubcategoryCommand command, CancellationToken cancellationToken)
        {
            var id = await _mediator.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPut("id")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] EditSubcategoryCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command with { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteSubcategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
