﻿using BudgetManager.Application.Exceptions;
using BudgetManager.Application.Interfaces;
using Mediator;

namespace BudgetManager.Application.Expenses.Commands
{
    public record EditExpenseCommand(Guid Id, decimal Amount, string Comment) : IRequest<Unit>;

    public class EditExpenseHandler : IRequestHandler<EditExpenseCommand, Unit>
    {
        private readonly IExpenseRepository _expenseRepository;
        public EditExpenseHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public ValueTask<Unit> Handle(EditExpenseCommand request, CancellationToken cancellationToken)
        {
            var expense = _expenseRepository.Get(request.Id) ?? throw new NotFoundException();

            expense.Amount = request.Amount;
            expense.Comment = request.Comment;

            _expenseRepository.Edit(expense);

            return new ValueTask<Unit>(Unit.Value);
        }
    }
}