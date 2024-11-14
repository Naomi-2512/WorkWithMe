namespace backend.Models
{
    public class Position
    {
        public string PositionId { get; set; }
        public string JobId { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public int RequiredUsers { get; set; }
        public string TimePeriod { get; set; }
        public Job Job { get; set; }
    }
}
