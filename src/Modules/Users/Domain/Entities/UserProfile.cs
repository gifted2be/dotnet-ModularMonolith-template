using Users.Domain.Entities.Bases;

namespace Users.Domain.Entities
{
    public class UserProfile: BaseDomain
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; } = default!;
        public DateTime Birthday { get; set; }

        public User? User { get; set; }
    }
}
