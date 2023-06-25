using Mediator;

namespace BudgetManager.Application.Categories
{
    public record GetCategoryQuery(Guid id) : IRequest<Unit>;

    public class GetCategoryHandler : IRequestHandler<GetCategoryQuery, Unit>
    {
        public ValueTask<Unit> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
