namespace backend.Dto.UserDto
{
    public class CreateUserDto
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Password { get; set; }
        public String ProfilePicture { get; set; }
        public string Role { get; set; }
    }
}
