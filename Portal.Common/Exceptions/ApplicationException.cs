﻿namespace Portal.Common.Exceptions
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message) { }
        public ApplicationException() { }
    }
}
