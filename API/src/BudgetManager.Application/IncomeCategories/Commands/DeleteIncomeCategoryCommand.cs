using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.IncomeCategories.Commands
{
    public record DeleteIncomeCategoryCommand(Guid Id) : IRequest<bool>;

    public class DeleteIncomeCategoryHandler : IRequestHandler<DeleteIncomeCategoryCommand, bool>
    {
        private readonly IIncomeCategoryRepository _repository;

        public DeleteIncomeCategoryHandler(IIncomeCategoryRepository repository)
        {
            _repository = repository;
        }

        public ValueTask<bool> Handle(DeleteIncomeCategoryCommand request, CancellationToken cancellationToken)
        {
            //Polityka usuwania - jeśli dana kategoria ma już zapisane wydatki - soft delete, usunięcie z listy, nie pokazywanie w nowych misiącach

            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            return new ValueTask<bool>(_repository.Delete(request.Id));
        }
    }
}
