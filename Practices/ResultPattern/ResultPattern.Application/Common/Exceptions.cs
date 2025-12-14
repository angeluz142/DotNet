namespace ResultPattern.Application.Common
{
    public class AppValidationException : Exception
    {
        public AppValidationException(string message) : base(message) { }
    }

    public class AppNotFoundException : Exception
    {
        public AppNotFoundException(string message) : base(message) { }
    }

    public class AppConflictException : Exception
    {
        public AppConflictException(string message) : base(message) { }
    }

}
