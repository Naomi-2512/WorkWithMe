namespace backend.Dto.JobPositionDto
{
    public class UpdateJobPositionDto
    {
        public string Title { get; set; }
        public int Price { get; set; }
        public int RequiredUsers { get; set; }
        public string TimePeriod { get; set; }
    }
}
