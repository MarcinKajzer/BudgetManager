using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Categories.Commands
{
    public record DeleteExpenseCategoryCommand(Guid Id) : IRequest<Unit>;

    public class DeleteExpenseCategoryHandler : IRequestHandler<DeleteExpenseCategoryCommand, Unit>
    {
        private readonly IExpenseCategoryRepository _repository;

        public DeleteExpenseCategoryHandler(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<Unit> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            //Polityka usuwania - jeśli dana kategoria ma już zapisane wydatki - soft delete, usunięcie z listy, nie pokazywanie w nowych misiącach

            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            await _repository.DeleteAsync(category, cancellationToken);
            return Unit.Value;
        }
    }
}
