namespace BudgetManager.Application.Exceptions
{
    internal class NotFoundException : CustomException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
    }
}
