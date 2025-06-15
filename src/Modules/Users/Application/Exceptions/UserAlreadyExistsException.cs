using ModularMonolith.Template.SharedKernel.Exceptions;

namespace Users.Application.Exceptions
{
    public class UserAlreadyExistsException : AppException
    {
        public override string Code => "user_exists";

        public UserAlreadyExistsException(string email)
            : base($"User with email '{email}' already exists.") { }
    }
}
