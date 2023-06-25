using Mediator;

namespace BudgetManager.Application.Categories
{
    public record DeleteCategoryCommand(Guid id) : IRequest<Unit>;

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        public ValueTask<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            //Polityka usuwania - jeśli dana kategoria ma już zapisane wydatki - soft delete, usunięcie z listy, nie pokazywanie w nowych misiącach
            throw new NotImplementedException();
        }
    }
}
