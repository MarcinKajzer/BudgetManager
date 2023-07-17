namespace BudgetManager.Domain.Incomes
{
    public class Income
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public Guid CategoryId { get; set; }
    }
}
