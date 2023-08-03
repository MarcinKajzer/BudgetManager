using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Commands
{
    public record DeleteIncomeCategoryCommand(Guid Id) : IRequest<Unit>;

    public class DeleteIncomeCategoryHandler : IRequestHandler<DeleteIncomeCategoryCommand, Unit>
    {
        private readonly IIncomeCategoryRepository _repository;

        public DeleteIncomeCategoryHandler(IIncomeCategoryRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<Unit> Handle(DeleteIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            //Polityka usuwania - jeśli dana kategoria ma już zapisane wydatki - soft delete, usunięcie z listy, nie pokazywanie w nowych misiącach

            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            await _repository.DeleteAsync(category, cancellationToken);
            return Unit.Value;
        }
    }
}
