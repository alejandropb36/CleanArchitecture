namespace CleanArchitecture.Application.Exceptions
{
    public class BadRequesException : ApplicationException
    {
        public BadRequesException(string message): base(message)
        {
        }
    }
}
