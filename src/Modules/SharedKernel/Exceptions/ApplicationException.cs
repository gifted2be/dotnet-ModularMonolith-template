namespace ModularMonolith.Template.SharedKernel.Exceptions
{
    public class AppException : BaseException
    {
        public override string Code => "application_error";
        public int ErrorCode { get; private set; } = 400;

        public AppException(string message) : base(message) { }

        public AppException(string message, int ErrorCode) : base(message) { 
            this.ErrorCode = ErrorCode;
        }

        public AppException(string message, Exception? innerException)
            : base(message, innerException) { }
    }
}
