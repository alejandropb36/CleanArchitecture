namespace CleanArchitecture.API.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? Deatils { get; set; }
        public CodeErrorException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Deatils = details;
        }
    }
}
