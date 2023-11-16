using BudgetManager.Application.Incomes.Commands;
using BudgetManager.Application.Incomes.Queries;
using BudgetManager.Application.Interfaces;
using Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IncomeController : ApplicationControllerBase
    {
        private readonly IIdStorage _idStorage;

        public IncomeController(IMediator mediator, IIdStorage idStorage) : base(mediator) => _idStorage = idStorage;

        [HttpPost]
        public async Task<IActionResult> Add(AddIncomeCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok(_idStorage.GetId());
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new GetIncomeQuery(id), cancellationToken));
        }
    
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateIncomeCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command with { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteIncomeCommand(id), cancellationToken);
            return NoContent();
        }
    }
}
