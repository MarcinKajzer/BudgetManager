using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Categories;
using Mediator;

namespace BudgetManager.Application.Categories.Commands
{
    public record AddExpenseCategoryCommand(string Name) : IRequest<Guid>;

    public class AddExpenseCategoryHandler : IRequestHandler<AddExpenseCategoryCommand, Guid>
    {
        private readonly IExpenseCategoryRepository _repository;
        public AddExpenseCategoryHandler(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<Guid> Handle(AddExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new ExpenseCategory
            {
                Name = request.Name,
                Id = Guid.NewGuid(),
            };

            _repository.Add(category);
            return new ValueTask<Guid>(category.Id); //jak dział value task i czy zwrotka nowego taska to dobre rozwiązanie ? 
        }
    }
}
