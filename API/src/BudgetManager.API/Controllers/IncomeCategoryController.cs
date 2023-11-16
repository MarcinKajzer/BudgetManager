using BudgetManager.Application.IncomeCategories.Commands;
using BudgetManager.Application.IncomeCategories.Queries;
using BudgetManager.Application.Interfaces;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeCategoryController : ApplicationControllerBase
    {
        private readonly IIdStorage _idStorage;

        public IncomeCategoryController(IMediator mediator, IIdStorage idStorage) : base(mediator) => _idStorage = idStorage;

        [HttpPost]
        public async Task<IActionResult> Add(AddIncomeCategoryCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(_idStorage.GetId());
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
        public async Task<IActionResult> Update(Guid id, UpdateIncomeCategoryCommand command, CancellationToken cancellationToken)
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
