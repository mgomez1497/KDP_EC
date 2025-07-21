namespace KDP_EC.Core.Models
{
    public class ActivityType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
        public string Description { get; set; }
    }
}
