using Mediator;

namespace BudgetManager.Application.Categories
{
    public record EditCategoryQuery(Guid id, string Name) : IRequest<Unit>;

    public class EditCategoryHandler : IRequestHandler<EditCategoryQuery, Unit>
    {
        public ValueTask<Unit> Handle(EditCategoryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
