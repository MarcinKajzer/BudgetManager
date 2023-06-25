using Mediator;

namespace BudgetManager.Application.Categories
{
    public record AddCategoryCommand(string Name) : IRequest<Unit>;

    public class AddCategoryHandler : IRequestHandler<AddCategoryCommand, Unit>
    {
        public ValueTask<Unit> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
