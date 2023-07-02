using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Categories.Commands
{
    public record EditCategoryCommand(Guid Id, string Name) : IRequest<bool>;

    public class EditCategoryHandler : IRequestHandler<EditCategoryCommand, bool>
    {
        private readonly ICategoryRepository _repository;

        public EditCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public ValueTask<bool> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            category.Name = request.Name;

            return new ValueTask<bool>(_repository.Update(category));
        }
    }
}
