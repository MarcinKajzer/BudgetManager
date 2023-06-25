using Mediator;

namespace BudgetManager.Application.Categories
{
    public record GetAllCategoriesQuery : IRequest<Unit>;

    public class GetAllCategoriesRequest : IRequestHandler<GetAllCategoriesQuery, Unit>
    {
        public ValueTask<Unit> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

