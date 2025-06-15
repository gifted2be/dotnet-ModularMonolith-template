namespace ModularMonolith.Template.SharedKernel.Auth
{
    public class JwtUserInfo
    {
        public Guid UserId { get; set; }
        public string Role { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
