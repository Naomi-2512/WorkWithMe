using backend.Models;

namespace backend.Dto.JobDto
{
    public class CreateJobDto
    {
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public string[] Position { get; set; }
        public string Category { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
    }
}
