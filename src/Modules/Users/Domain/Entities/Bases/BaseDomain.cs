namespace Users.Domain.Entities.Bases
{
    public abstract class BaseDomain
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
