using backend.Data;
using backend.Dto.LoginsDto;
using backend.Interfaces;
using backend.Models;
using backend.RepositoryResults;

namespace backend.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public RepositoryResult<User> GetUsers()
        {
            List<User> users = dataContext.Users.OrderBy(user => user.CreatedAt).ToList();

            if (users == null) return new RepositoryResult<User>(false, "Unable to locate available users", new List<User>());

            if (users.Count == 0) return new RepositoryResult<User>(false, "No users available at the moment", new List<User>());

            return new RepositoryResult<User>(true, "Users retrieved successfully", users);
        }

        public RepositoryResult<User> GetUserById(string UserId)
        {
            User? user = dataContext.Users.FirstOrDefault(user => user.UserId == UserId);

            if (user == null) return new RepositoryResult<User>(false, "User specified not found", new List<User>());

            return new RepositoryResult<User>(true, $"{user.Fullname.Split(' ')[0].ToUpper()} retrieved successfully", user);
        }

        public RepositoryResult<Approval> GetUsersByJobApplication(string JobId)
        {
            var users = dataContext.Approvals.Where(user => user.JobId == JobId).OrderBy(approval => approval.ApprovalId).ToList();

            if (users == null) return new RepositoryResult<Approval>(false, "Unable to locate available appliers for this job.", new List<Approval>());

            if (users.Count == 0) return new RepositoryResult<Approval>(true, "No users available at the moment", new List<Approval>());

            return new RepositoryResult<Approval>(true, "Users retrieved successfully", users);
        }

        public bool Save()
        {
            return dataContext.SaveChanges() > 1;
        }

        public RepositoryResult<User> DeleteUser(string UserId)
        {
            if (string.IsNullOrWhiteSpace(UserId)) return new RepositoryResult<User>(false, "User identification is invalid", new List<User>());

            var user = dataContext.Users.FirstOrDefault(user => user.UserId == UserId);

            if (user == null) return new RepositoryResult<User>(false, "The specified user is not found", new List<User>());

            dataContext.Users.Remove(user);

            if (!Save()) return new RepositoryResult<User>(false, $"Unable to delete {user.Fullname.Split(' ')[0]} at the moment", new List<User>());

            return new RepositoryResult<User>(true, $"{user.Fullname.Split(' ')[0]} deleted successfully", new List<User>());
        }

        public RepositoryResult<User> CreateUser(User user)
        {
            if(user == null) return new RepositoryResult<User>(false, "Input credatials should be valid.", new List<User>());

            User? emailExist = dataContext.Users.FirstOrDefault(user=>user.Email == user.Email);

            if (emailExist != null) return new RepositoryResult<User>(false, "The email provided exist, try loging in instead.", new List<User>());

            User? phoneExists = dataContext.Users.FirstOrDefault(user => user.Mobile == user.Mobile);

            if (phoneExists != null) return new RepositoryResult<User>(false, "The phone number provided exists, login to continue", new List<User>());

            dataContext.Users.Add(user);

            if (!Save()) return new RepositoryResult<User>(false, "Unable to register user at the moment, try again later", new List<User>());

            return new RepositoryResult<User>(true, "Account created successfully, Welcome.", new List<User>());
        }

        public RepositoryResult<User> UpdateUser(string UserId, User user)
        {
            if (string.IsNullOrWhiteSpace(UserId)) return new RepositoryResult<User>(false, "User idenification provided is invalid.", new List<User>());

            User? _user = dataContext.Users.FirstOrDefault(user => user.UserId == UserId);

            if (_user == null) return new RepositoryResult<User>(false, "User not found.", new List<User>());

            User? emailExist = dataContext.Users.FirstOrDefault(user => (user.Email != _user.Email));

            if (emailExist != null) return new RepositoryResult<User>(false, "The email you changed exists, update using new email.", new List<User>());

            User? phoneExists = dataContext.Users.FirstOrDefault(user => user.Mobile != _user.Mobile);

            if (phoneExists != null) return new RepositoryResult<User>(false, "The phone number changed exists, update using new phone number", new List<User>());

            bool passwordMatches = BCrypt.Net.BCrypt.Verify(user.Password, _user.Password);

            if (!passwordMatches) return new RepositoryResult<User>(false, "Password provided is incorrect.", new List<User>());

            _user.Fullname = user.Fullname;
            _user.Email = user.Email;
            _user.Mobile = user.Mobile;
            _user.Country = user.Country;
            _user.City = user.City;
            _user.ProfilePicture = user.ProfilePicture;
            _user.Role = user.Role;

            if (!Save()) return new RepositoryResult<User>(false, "Unable to update details at the moment, try again later", new List<User>());

            return new RepositoryResult<User>(true, "Profile updated successfully.", new List<User>());
        }

        public RepositoryResult<User> LoginUser(LoginDetails loginDetails)
        {
            if(loginDetails == null) return new RepositoryResult<User>(false, "Details entered contains invalid values.", new List<User>());

            User? user = dataContext.Users.FirstOrDefault(user => user.Email == loginDetails.Email);

            if (user == null) return new RepositoryResult<User>(false, "Email not found, Register to login.", new List<User>());

            bool PasswordMatches = BCrypt.Net.BCrypt.Verify(loginDetails.Password, user.Password);

            if (!PasswordMatches) return new RepositoryResult<User>(false, "Incorrect Password", new List<User>());

            return new RepositoryResult<User>(true, "Welcome back, login successful", user);
        }
    }
}
