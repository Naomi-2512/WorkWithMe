namespace backend.Dto.PaymentDto
{
    public class MakePaymentDto
    {
        public string PaymentId { get; set; }
        public string JobId { get; set; }
        public string UserId { get; set; }
        public bool IsVerified { get; set; }
        public int Price { get; set; }
        public int AdditionalPrice { get; set; }
    }
}
