using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Categories.Commands
{
    public record EditExpenseCategoryCommand(Guid Id, string Name) : IRequest<bool>;

    public class EditExpenseCategoryHandler : IRequestHandler<EditExpenseCategoryCommand, bool>
    {
        private readonly IExpenseCategoryRepository _repository;

        public EditExpenseCategoryHandler(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        public ValueTask<bool> Handle(EditExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            category.Name = request.Name;

            return new ValueTask<bool>(_repository.Update(category));
        }
    }
}
