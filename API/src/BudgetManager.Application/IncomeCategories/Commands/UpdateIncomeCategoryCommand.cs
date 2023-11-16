using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Commands
{
    public record UpdateIncomeCategoryCommand(Guid Id, string Name) : ICommand;

    public class UpdateIncomeCategoryHandler : ICommandHandler<UpdateIncomeCategoryCommand>
    {
        private readonly IIncomeCategoryRepository _repository;

        public UpdateIncomeCategoryHandler(IIncomeCategoryRepository repository) => _repository = repository;
        
        public async ValueTask<Unit> Handle(UpdateIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            category.Name = request.Name;

            await _repository.UpdateAsync(category, cancellationToken);
            return Unit.Value;
        }
    }
}
