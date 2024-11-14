using backend.Data;
using backend.Dto.JobDto;
using backend.Interfaces;
using backend.Models;
using backend.RepositoryResults;

namespace backend.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly DataContext dataContext;

        public JobRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public RepositoryResult<Job> GetJobs()
        {
            var jobs = dataContext.Jobs.OrderBy(job => job.JobId).ToList();

            if (jobs.Count < 0) return new RepositoryResult<Job>(false, "No jobs found at the moment", new List<Job>());

            return new RepositoryResult<Job>(true, "All jobs fetched successfully.", jobs);
        }

        public RepositoryResult<Job> GetActivatedJobs()
        {
            var jobs = dataContext.Jobs.Where(job => job.IsActivated == true).OrderBy(job => job.JobId).ToList();

            if (jobs.Count < 0) return new RepositoryResult<Job>(false, "No jobs found at the moment", new List<Job>());

            return new RepositoryResult<Job>(true, "All jobs fetched successfully", jobs);
        }

        public bool Save() => dataContext.SaveChanges() > 0;

        public RepositoryResult<Job> CreateJob(string UserId, Job job)
        {
            if (string.IsNullOrWhiteSpace(UserId)) return new RepositoryResult<Job>(false, "User identification not verified", new List<Job>());

            var user = dataContext.Users.FirstOrDefault(user => user.UserId == UserId);

            if (user == null) return new RepositoryResult<Job>(false, "You are not authorised to access this service", new List<Job>());

            if(job == null) return new RepositoryResult<Job>(false, "Job values cannot be empty", new List<Job>());

            dataContext.Jobs.Add(job);

            if (!Save()) return new RepositoryResult<Job>(false, $"Unable to create {job.Title} at the moment.", new List<Job>());

            return new RepositoryResult<Job>(true, $"{job.Title} created successfully.", job);

        }

        public RepositoryResult<Job> UpdateJob(string JobId,  Job job)
        {
            if (string.IsNullOrWhiteSpace(JobId)) return new RepositoryResult<Job>(false, "Invalid job identification entered", new List<Job>());

            if(job == null) return new RepositoryResult<Job>(false, "Job update cannot contain empty values", new List<Job>());

            dataContext.Jobs.Update(job);

            if (!Save()) return new RepositoryResult<Job>(false, $"Unable to update {job.Title} at the moment", job);

            return new RepositoryResult<Job>(true, $"{job.Title} successfully updated.", job);
        }

        public RepositoryResult<Job> DeleteJob(string JobId)
        {
            if(string.IsNullOrWhiteSpace(JobId)) return new RepositoryResult<Job>(false, "Invalid job identification", new List<Job>());

            Job? job = dataContext.Jobs.FirstOrDefault(job => job.JobId == JobId);

            if(job == null) return new RepositoryResult<Job>(false, "The job specified not found.", new List<Job>());

            dataContext.Jobs.Remove(job);

            if (!Save()) return new RepositoryResult<Job>(false, "Unable to delete job at the moment", new List<Job>());

            return new RepositoryResult<Job>(true, "Job successfully deleted.", job);
        }

        public RepositoryResult<Job> GetJobById(string JobId)
        {
            if(string.IsNullOrWhiteSpace(JobId)) return new RepositoryResult<Job>(false, "Invalid job identification passed.", new List<Job>());

            Job? job = dataContext.Jobs.FirstOrDefault(job => JobId == job.JobId);

            if (job == null) return new RepositoryResult<Job>(false, "Job specified is not found", new List<Job>());

            return new RepositoryResult<Job>(true, $"{job.Title} retrieved successfully.", job);
        }

        public RepositoryResult<List<Job>> GetJobByUserId(string UserId)
        {
            if (string.IsNullOrWhiteSpace(UserId)) return new RepositoryResult<List<Job>>(false, "Invalid user identification, try again later", new List<List<Job>>());

            var user = dataContext.Users.FirstOrDefault(user => user.UserId.Equals(UserId));

            if(user == null) return new RepositoryResult<List<Job>>(false, "This action is not authorised at the moment", new List<List<Job>>());

            var jobs = dataContext.Jobs.Where(job => job.UserId.Equals(UserId)).OrderBy(job => job.JobId).ToList();

            if (jobs.Count == 0) return new RepositoryResult<List<Job>>(false, "You have no jobs at the moment", jobs);

            return new RepositoryResult<List<Job>>(true, $"Your jobs, {user.Fullname} have been successfully fetched", jobs);
        }

        public RepositoryResult<Job> UpdateJobStatus(string JobId)
        {
            if(string.IsNullOrWhiteSpace(JobId)) return new RepositoryResult<Job>(false, "Job identification is invalid", new List<Job>());

            Job? job = dataContext.Jobs.FirstOrDefault(job => job.JobId == JobId);

            if(job == null) return new RepositoryResult<Job>(false, "Job specified is not found", new List<Job>());

            job.IsActivated = true;

            dataContext.Jobs.Update(job);

            if (!Save()) return new RepositoryResult<Job>(false, $"Unable to activate {job.Title}, try again later", new List<Job>());

            return new RepositoryResult<Job>(true, $"{job.Title} was successfully activated. Thank you", job);
        }
    }
}
