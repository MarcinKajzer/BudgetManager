using BudgetManager.Application.ExpenseSubcategories.Commands;
using BudgetManager.Application.Interfaces;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExpenseSubcategoryController : ApplicationControllerBase
    {
        private readonly IIdStorage _idStorage;

        public ExpenseSubcategoryController(IMediator mediator, IIdStorage idStorage) : base(mediator) => _idStorage = idStorage;

        [HttpPost]
        public async Task<IActionResult> Add(AddSubcategoryCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(_idStorage.GetId());
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateSubcategoryCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command with { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteSubcategoryCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
