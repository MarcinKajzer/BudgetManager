using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Commands
{
    public record EditIncomeCategoryCommand(Guid Id, string Name) : IRequest<bool>;

    public class EditIncomeCategoryHandler : IRequestHandler<EditIncomeCategoryCommand, bool>
    {
        private readonly IIncomeCategoryRepository _repository;

        public EditIncomeCategoryHandler(IIncomeCategoryRepository repository)
        {
            _repository = repository;
        }

        public ValueTask<bool> Handle(EditIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            category.Name = request.Name;

            return new ValueTask<bool>(_repository.Update(category));
        }
    }
}
