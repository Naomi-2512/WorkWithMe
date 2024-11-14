namespace backend.Models
{
    public class Job
    {
        public string JobId { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string[] JobPositions { get; set; }
        public string Category { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActivated { get; set; }
        public ICollection<Position> Positions { get; set; }
        public ICollection<Approval> Approvals { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public User User { get; set; }
    }
}
