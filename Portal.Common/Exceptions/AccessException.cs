namespace Portal.Common.Exceptions
{
    public class AccessException : ApplicationException
    {
        public AccessException(string message = "Restricted access.") : base(message) { }
    }
}
