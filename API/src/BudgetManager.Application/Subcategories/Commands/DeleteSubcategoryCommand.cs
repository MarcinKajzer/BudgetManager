using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Subcategories.Commands
{
    public record DeleteSubcategoryCommand(Guid Id) : IRequest<Unit>;

    public class DeleteSubcategoryHandler : IRequestHandler<DeleteSubcategoryCommand, Unit>
    {
        private readonly ISubcategoryRepository _repository;
        public DeleteSubcategoryHandler(ISubcategoryRepository repository)
        {
            _repository = repository;
        }
        public ValueTask<Unit> Handle(DeleteSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var subcategory = _repository.Get(request.Id) ?? throw new NotFoundException();
            _repository.Delete(subcategory.Id); // możnaby przekazać cały obiekt zamist tylko Id

            return new ValueTask<Unit>(Unit.Value);
        }
    }
}