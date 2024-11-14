using backend.Models;
using backend.RepositoryResults;

namespace backend.Interfaces
{
    public interface IJobRepository
    {
        bool Save();
        RepositoryResult<Job> GetJobs();
        RepositoryResult<Job> GetActivatedJobs();
        RepositoryResult<List<Job>> GetJobByUserId(string UserId);
        RepositoryResult<Job> GetJobById(string JobId);
        RepositoryResult<Job> CreateJob(string UserId, Job job);
        RepositoryResult<Job> UpdateJob(string JobId, Job job);
        RepositoryResult<Job> DeleteJob(string JobId);
        RepositoryResult<Job> UpdateJobStatus(string JobId);
    }
}