namespace backend.Models
{
    public class User
    {
        public string UserId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public String ProfilePicture { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Approval> Approvals { get; set; }
    }
}
