using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Categories
{
    public record DeleteCategoryCommand(Guid Id) : IRequest<bool>;

    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public ValueTask<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            //Polityka usuwania - jeśli dana kategoria ma już zapisane wydatki - soft delete, usunięcie z listy, nie pokazywanie w nowych misiącach

            return new ValueTask<bool>(_repository.Delete(request.Id));
        }
    }
}
