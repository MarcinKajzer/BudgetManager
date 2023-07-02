namespace BudgetManager.Application.Exceptions
{
    internal class CustomException : Exception
    {
        public CustomException() { }
        public CustomException(string message) : base(message) { }
    }
}
