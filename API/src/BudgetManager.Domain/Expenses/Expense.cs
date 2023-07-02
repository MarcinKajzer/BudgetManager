namespace BudgetManager.Domain.Expenses
{
    public class Expense
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public Guid SubcategoryId { get; set; }
    }
}
