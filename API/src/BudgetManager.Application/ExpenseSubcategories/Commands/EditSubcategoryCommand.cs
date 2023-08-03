using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Subcategories.Commands
{
    public record EditSubcategoryCommand(Guid Id, string Name) : IRequest<Unit>;

    public class EditSubcategoryHandler : IRequestHandler<EditSubcategoryCommand, Unit>
    {
        private readonly IExpenseSubcategoryRepository _repository;

        public EditSubcategoryHandler(IExpenseSubcategoryRepository repository)
        {
            _repository = repository;
        }
        public async ValueTask<Unit> Handle(EditSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var subcategory = _repository.Get(request.Id) ?? throw new NotFoundException();
            subcategory.Name = request.Name;
            
            await _repository.UpdateAsync(subcategory, cancellationToken);
            return Unit.Value;
        }
    }
}
