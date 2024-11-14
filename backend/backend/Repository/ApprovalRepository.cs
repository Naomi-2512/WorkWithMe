using backend.Data;
using backend.Interfaces;
using backend.Models;
using backend.RepositoryResults;

namespace backend.Repository
{
    public class ApprovalRepository : IApprovalRepository
    {
        private readonly DataContext dataContext;

        public ApprovalRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool Save() => dataContext.SaveChanges() > 1;

        public RepositoryResult<Approval> CreateApproval(Approval approval)
        {
            if (approval == null) return new RepositoryResult<Approval>(false, "All aproval details cannot be invalid", new List<Approval>());

            bool IdsValid = (!string.IsNullOrWhiteSpace(approval.ApprovalId) && !string.IsNullOrWhiteSpace(approval.JobId) && !string.IsNullOrWhiteSpace(approval.OwnerId) && !string.IsNullOrWhiteSpace(approval.ApplierId));

            if (!IdsValid) return new RepositoryResult<Approval>(false, "Incorrect identifications passed, try again later", new List<Approval>());

            dataContext.Approvals.Add(approval);

            if (!Save()) return new RepositoryResult<Approval>(false, "Unable to create job approval at the moment", new List<Approval>());

            return new RepositoryResult<Approval>(Save(), "Job approval submitted successfully.", new List<Approval>());

        }

        public RepositoryResult<Approval> UpdateApproval(string ApprovalId, Approval approval)
        {
            if (string.IsNullOrWhiteSpace(ApprovalId)) return new RepositoryResult<Approval>(false, "Request contains invalid identification", new List<Approval>());

            if (approval.PositionApplied == null) return new RepositoryResult<Approval>(false, "Update approval position to update", new List<Approval>());

            Approval? _approval = dataContext.Approvals.FirstOrDefault(approval => approval.ApprovalId == ApprovalId);

            if (_approval == null) return new RepositoryResult<Approval>(false, "Approval specified is not found", new List<Approval>());

            _approval.PositionApplied = approval.PositionApplied;

            if (!Save()) return new RepositoryResult<Approval>(false, "Unable to update job approval at the moment", new List<Approval>());

            return new RepositoryResult<Approval>(Save(), "Job approval updated successfully.", new List<Approval>());

        }

        public RepositoryResult<Approval> DeleteApproval(string ApprovalId)
        {
            if (string.IsNullOrWhiteSpace(ApprovalId)) return new RepositoryResult<Approval>(false, "Approval identification is invalid.", new List<Approval>());

            Approval? approval = dataContext.Approvals.FirstOrDefault(approval => approval.ApprovalId == ApprovalId);

            if (approval == null) return new RepositoryResult<Approval>(false, "Approval specified is not found.", new List<Approval>());

            dataContext.Approvals.Remove(approval);

            if (!Save()) return new RepositoryResult<Approval>(false, "Unable to dismiss approval at the moment.", new List<Approval>());

            return new RepositoryResult<Approval>(Save(), "Approval successfully dismissed", new List<Approval>());
        }

        public RepositoryResult<Approval> GetApprovals()
        {
            List<Approval>? approvals = dataContext.Approvals.OrderBy(approval => approval.ApprovalId).ToList();

            if (approvals == null) return new RepositoryResult<Approval>(false, "Unable to fetch approvals at the moment.", new List<Approval>());

            if(approvals.Count == 0) return new RepositoryResult<Approval>(false, "No approvals found.", approvals);

            return new RepositoryResult<Approval>(true, "Approvals retrieved successfully", approvals);
        }

        public RepositoryResult<Approval> GetApproval(string ApprovalId)
        {
            if (string.IsNullOrWhiteSpace(ApprovalId)) return new RepositoryResult<Approval>(false, "Approval identification is invalid", new List<Approval>());

            Approval? approval = dataContext.Approvals.FirstOrDefault(approval => approval.ApprovalId == ApprovalId);

            if (approval == null) return new RepositoryResult<Approval>(false, "The approval specified not found", new List<Approval>());

            return new RepositoryResult<Approval>(true, "Approval success retrieved", approval);
        }

        public RepositoryResult<Approval> GetApprovalByUserId(string UserId)
        {
            if (string.IsNullOrWhiteSpace(UserId)) return new RepositoryResult<Approval>(false, "Approval identification is invalid.", new List<Approval>());

            User? user = dataContext.Users.FirstOrDefault(user => user.UserId == UserId);

            if (user == null) return new RepositoryResult<Approval>(false, "This action is not authorised, login and try again.", new List<Approval>());

            List<Approval> approvals = dataContext.Approvals.Where(user => (user.Equals(UserId) && user.IsRejected == false)).OrderBy(approval => approval.ApprovalId).ToList();

            if (approvals == null) return new RepositoryResult<Approval>(false, $"{user.Fullname.Split(' ')[0]}, we are unable to locate your applied jobs.", new List<Approval>());

            if (approvals.Count == 0) return new RepositoryResult<Approval>(false, $"{user.Fullname.Split(' ')[0]}, you have no applied jobs at the moment.", new List<Approval>());

            return new RepositoryResult<Approval>(true, "Approval success retrieved", approvals);
        }

        public RepositoryResult<Approval> GetDeclinedApprovalByUserId(string UserId)
        {
            if (string.IsNullOrWhiteSpace(UserId)) return new RepositoryResult<Approval>(false, "Approval identification is invalid.", new List<Approval>());

            User? user = dataContext.Users.FirstOrDefault(user => user.UserId == UserId);

            if (user == null) return new RepositoryResult<Approval>(false, "This action is not authorised, login and try again.", new List<Approval>());

            List<Approval> approvals = dataContext.Approvals.Where(user => (user.Equals(UserId) && user.IsRejected == true)).OrderBy(approval => approval.ApprovalId).ToList();

            if (approvals == null) return new RepositoryResult<Approval>(false, $"{user.Fullname.Split(' ')[0]}, we are unable to locate your applied jobs.", new List<Approval>());

            if (approvals.Count == 0) return new RepositoryResult<Approval>(false, $"{user.Fullname.Split(' ')[0]}, you have no applied jobs at the moment.", new List<Approval>());

            return new RepositoryResult<Approval>(true, "Approval success retrieved", approvals);
        }

        public RepositoryResult<Approval> GetApprovalByJobId(string JobId)
        {
            if (string.IsNullOrWhiteSpace(JobId)) return new RepositoryResult<Approval>(false, "Approval identification is invalid.", new List<Approval>());

            Job? job = dataContext.Jobs.FirstOrDefault(job => job.JobId == JobId);

            if (job == null) return new RepositoryResult<Approval>(false, "Job is not found at the moment", new List<Approval>());

            List<Approval> approvals = dataContext.Approvals.Where(job => (job.Equals(JobId) && job.IsRejected == false)).OrderBy(approval => approval.ApprovalId).ToList();

            if (approvals == null) return new RepositoryResult<Approval>(false, $"We are unable to locate your appliers for {job.Title.Split(' ')[0]} job.", new List<Approval>());

            if (approvals.Count == 0) return new RepositoryResult<Approval>(false, $"No appliers currently available for {job.Title.Split(' ')[0]}.", new List<Approval>());

            return new RepositoryResult<Approval>(true, "Appliers successfully retrieved", approvals);
        }
    }
}
