using BudgetManager.Application.Categories;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApplicationControllerBase
    {
        public CategoryController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]AddCategoryCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpGet("id")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetCategoryQuery(id), cancellationToken));
        }

        [HttpGet]
        public async Task<IActionResult> GetALl(CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery(), cancellationToken));

        }

        [HttpPut]
        public async Task<IActionResult> Edit(Guid id, [FromBody]EditCategoryQuery query, CancellationToken cancellationToken)
        {
            await _mediator.Send(query, cancellationToken);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteCategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
