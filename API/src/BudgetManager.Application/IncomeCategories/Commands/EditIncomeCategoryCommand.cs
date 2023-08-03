using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Commands
{
    public record EditIncomeCategoryCommand(Guid Id, string Name) : IRequest<Unit>;

    public class EditIncomeCategoryHandler : IRequestHandler<EditIncomeCategoryCommand, Unit>
    {
        private readonly IIncomeCategoryRepository _repository;

        public EditIncomeCategoryHandler(IIncomeCategoryRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<Unit> Handle(EditIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            category.Name = request.Name;

            await _repository.UpdateAsync(category, cancellationToken);
            return Unit.Value;
        }
    }
}
