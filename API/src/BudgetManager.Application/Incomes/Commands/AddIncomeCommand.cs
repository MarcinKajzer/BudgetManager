using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using BudgetManager.Domain.Incomes;
using Mediator;

namespace BudgetManager.Application.Incomes.Commands
{
    public record AddIncomeCommand(Guid CategoryId, DateTime Date, decimal Amount, string Comment) : ICommand;

    public class AddIncomeHandler : ICommandHandler<AddIncomeCommand>
    {
        private readonly IIncomeCategoryRepository _categoryRepository;
        private readonly IIncomeRepository _incomeRepository;
        private readonly IIdStorage _idStorage;

        public AddIncomeHandler(IIncomeCategoryRepository categoryRepository, IIncomeRepository incomeRepository, IIdStorage idStorage)
        {
            _categoryRepository = categoryRepository;
            _incomeRepository = incomeRepository;
            _idStorage = idStorage;
        }

        public async ValueTask<Unit> Handle(AddIncomeCommand request, CancellationToken cancellationToken)
        {
            var category = _categoryRepository.Get(request.CategoryId) ?? throw new NotFoundException();

            var income = new Income
            {
                Amount = request.Amount,
                Comment = request.Comment,
                Date = request.Date,
                Category = category
            };

            await _incomeRepository.CreateAsync(income, cancellationToken);
            _idStorage.SetId(category.Id);
            
            return Unit.Value;
        }
    }
}
