using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Subcategories.Commands
{
    public record EditSubcategoryCommand(Guid Id, string Name) : IRequest<Unit>;

    public class EditSubcategoryHandler : IRequestHandler<EditSubcategoryCommand, Unit>
    {
        private readonly ISubcategoryRepository _repository;

        public EditSubcategoryHandler(ISubcategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<Unit> Handle(EditSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var subcategory = _repository.Get(request.Id) ?? throw new NotFoundException();
            subcategory.Name = request.Name;
            _repository.Update(subcategory);

            return new ValueTask<Unit>(Unit.Value);
        }
    }
}
