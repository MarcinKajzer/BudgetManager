﻿using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Expenses.Commands
{
    public record UpdateExpenseCommand(Guid Id, decimal Amount, string Comment) : ICommand;

    public class UpdateExpenseHandler : ICommandHandler<UpdateExpenseCommand>
    {
        private readonly IExpenseRepository _expenseRepository;
        public UpdateExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async ValueTask<Unit> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetAsync(request.Id, cancellationToken) ?? throw new NotFoundException();

            expense.Amount = request.Amount;
            expense.Comment = request.Comment;

            await _expenseRepository.UpdateAsync(expense, cancellationToken);
            return Unit.Value;
        }
    }
}
