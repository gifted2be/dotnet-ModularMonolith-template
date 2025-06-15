using Auth.Application.DTOs.RequestModel;
using Users.Application.DTOs;

namespace ModularMonolith.Template.Application.Tests.Common
{
    public static class TestUsers
    {
        public static readonly string TestGuid = "644fb447-0a6f-4c19-8c05-78337d84eb41";

        public static readonly UserDto TestUser = new UserDto
        {
            Id = Guid.Parse(TestGuid),
            UserName = "Test User",
            Email = "test@yahoo.com",
            Password = "123"
        };

        public static readonly RegisterDto RegisterDto = new RegisterDto
        {
            UserName = "Test User",
            Email = "test@yahoo.com",
            Password = "123"
        };
    }
}
