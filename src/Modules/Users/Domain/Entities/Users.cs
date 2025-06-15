using Users.Domain.Entities.Bases;

namespace Users.Domain.Entities
{
    public class User: BaseDomain
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserProfile? Proflie { get; set; }
    }
}
