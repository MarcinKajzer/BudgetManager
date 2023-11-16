using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Categories.Commands
{
    public record UpdateExpenseCategoryCommand(Guid Id, string Name) : IRequest<Unit>;

    public class UpdateExpenseCategoryHandler : IRequestHandler<UpdateExpenseCategoryCommand, Unit>
    {
        private readonly IExpenseCategoryRepository _repository;

        public UpdateExpenseCategoryHandler(IExpenseCategoryRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<Unit> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = _repository.Get(request.Id) ?? throw new NotFoundException();
            category.Name = request.Name;

            await _repository.UpdateAsync(category, cancellationToken);
            return Unit.Value;
        }
    }
}
