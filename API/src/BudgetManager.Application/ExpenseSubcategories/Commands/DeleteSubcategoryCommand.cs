using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.ExpenseSubcategories.Commands
{
    public record DeleteSubcategoryCommand(Guid Id) : ICommand;

    public class DeleteSubcategoryHandler : ICommandHandler<DeleteSubcategoryCommand>
    {
        private readonly IExpenseSubcategoryRepository _repository;
        
        public DeleteSubcategoryHandler(IExpenseSubcategoryRepository repository) => _repository = repository;

        public async ValueTask<Unit> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var subcategory = _repository.Get(request.Id) ?? throw new NotFoundException();
            await _repository.DeleteAsync(subcategory, cancellationToken);

            return Unit.Value;
        }
    }
}