using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Commands
{
    public record AddIncomeCategoryCommand(string Name) : ICommand;

    public class AddIncomeCategoryHandler : ICommandHandler<AddIncomeCategoryCommand>
    {
        private readonly IIncomeCategoryRepository _repository;
        private readonly IIdStorage _idStorage;
        
        public AddIncomeCategoryHandler(IIncomeCategoryRepository repository, IIdStorage idStorage)
        {
            _repository = repository;
            _idStorage = idStorage;
        }
        
        public async ValueTask<Unit> Handle(AddIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new IncomeCategory
            {
                Name = request.Name,
            };

            await _repository.CreateAsync(category, cancellationToken);
            _idStorage.SetId(category.Id);    
            
            return Unit.Value;
        }
    }
}
