using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Categories
{
    public record EditCategoryQuery(Guid Id, string Name) : IRequest<bool>;

    public class EditCategoryHandler : IRequestHandler<EditCategoryQuery, bool>
    {
        private readonly ICategoryRepository _repository;

        public EditCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public ValueTask<bool> Handle(EditCategoryQuery request, CancellationToken cancellationToken)
        {
            return new ValueTask<bool>(_repository.Update(request.Id, request.Name));
        }
    }
}
