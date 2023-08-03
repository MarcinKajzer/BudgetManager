using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Categories.Commands
{
    public record EditExpenseCategoryCommand(Guid Id, string Name) : IRequest<Unit>;

    public class EditExpenseCategoryHandler : IRequestHandler<EditExpenseCategoryCommand, Unit>
    {
        private readonly IExpenseCategoryRepository _repository;

        public EditExpenseCategoryHandler(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<Unit> Handle(EditExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            category.Name = request.Name;

            await _repository.UpdateAsync(category, cancellationToken);
            return Unit.Value;
        }
    }
}
