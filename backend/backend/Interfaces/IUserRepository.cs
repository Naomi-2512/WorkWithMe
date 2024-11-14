using backend.Dto.LoginsDto;
using backend.Models;
using backend.RepositoryResults;

namespace backend.Interfaces
{
    public interface IUserRepository
    {
        RepositoryResult<User> GetUsers();
        RepositoryResult<User> GetUserById(string UserId);
        bool Save();
        RepositoryResult<User> CreateUser(User User);
        RepositoryResult<User> UpdateUser(string UserId, User User);
        RepositoryResult<User> LoginUser(LoginDetails loginDetails);
        RepositoryResult<User> DeleteUser(string UserId);
        RepositoryResult<Approval> GetUsersByJobApplication(string JobId);
    }
}
