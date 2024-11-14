namespace backend.Models
{
    public class Payment
    {
        public string PaymentId { get; set; }
        public string JobId { get; set; }
        public string UserId { get; set; }
        public bool IsVerified { get; set; }
        public int Price { get; set; }
        public int AdditionalPrice {  get; set; }
        public User User { get; set; }
        public Job Job { get; set; }
    }
}
