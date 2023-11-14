using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Subcategories.Commands
{
    public record UpdateSubcategoryCommand(Guid Id, string Name) : IRequest<Unit>;

    public class UpdateSubcategoryHandler : IRequestHandler<UpdateSubcategoryCommand, Unit>
    {
        private readonly IExpenseSubcategoryRepository _repository;

        public UpdateSubcategoryHandler(IExpenseSubcategoryRepository repository)
        {
            _repository = repository;
        }
        public async ValueTask<Unit> Handle(UpdateSubcategoryCommand request, CancellationToken cancellationToken)
        {
            var subcategory = _repository.Get(request.Id) ?? throw new NotFoundException();
            subcategory.Name = request.Name;
            
            await _repository.UpdateAsync(subcategory, cancellationToken);
            return Unit.Value;
        }
    }
}
