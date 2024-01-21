namespace Portal.Common.Exceptions
{
    public class InternalServiceException : ApplicationException
    {
        public override string Message { get; }
        public InternalServiceException(string message = "Response status code does not indicate success.")
        {
            Message = message;
        }
    }
}
