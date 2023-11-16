using BudgetManager.Application.Categories.Commands;
using BudgetManager.Application.Categories.Queries;
using BudgetManager.Application.ExpenseCategories.Commands;
using BudgetManager.Application.Interfaces;
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
        private readonly IIdStorage _idStorage;

        public ExpenseCategoryController(IMediator mediator, IIdStorage idStorage) : base(mediator) => _idStorage = idStorage;
        

        [HttpPost]
        public async Task<IActionResult> Add(AddExpenseCategoryCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(_idStorage.GetId());
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
        public async Task<IActionResult> Update(Guid id, UpdateExpenseCategoryCommand command, CancellationToken cancellationToken)
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
