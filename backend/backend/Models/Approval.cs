using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Approval
    {
        [Key]
        public string ApprovalId { get; set; }

        public string JobId { get; set; }
        public string OwnerId { get; set; }
        public string ApplierId { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public string PositionApplied { get; set; }
        public User Owner { get; set; }
        public User Applier { get; set; }
        public Job Job { get; set; }
    }
}
