namespace backend.Dto.ApprovalDto
{
    public class CreateApprovalDto
    {
        public string ApprovalId { get; set; }
        public string JobId { get; set; }
        public string OwnerId { get; set; }
        public string ApplierId { get; set; }
        public string PositionApplied { get; set; }
    }
}
