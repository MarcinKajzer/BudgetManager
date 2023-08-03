using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Commands
{
    public record AddIncomeCategoryCommand(string Name) : IRequest<Guid>;

    public class AddIncomeCategoryHandler : IRequestHandler<AddIncomeCategoryCommand, Guid>
    {
        private readonly IIncomeCategoryRepository _repository;
        public AddIncomeCategoryHandler(IIncomeCategoryRepository repository)
        {
            _repository = repository;
        }
        public async ValueTask<Guid> Handle(AddIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new IncomeCategory
            {
                Name = request.Name,
            };

            await _repository.CreateAsync(category, cancellationToken);
            return category.Id;
        }
    }
}
