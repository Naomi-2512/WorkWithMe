using backend.Models;
using backend.RepositoryResults;

namespace backend.Interfaces
{
    public interface IApprovalRepository
    {
        bool Save();
        RepositoryResult<Approval> CreateApproval(Approval approval);
        RepositoryResult<Approval> UpdateApproval(string ApprovalId, Approval approval);
        RepositoryResult<Approval> DeleteApproval(string ApprovalId);
        RepositoryResult<Approval> GetApproval(string ApprovalId);
        RepositoryResult<Approval> GetApprovalByUserId(string UserId);
        RepositoryResult<Approval> GetDeclinedApprovalByUserId(string UserId);
        RepositoryResult<Approval> GetApprovalByJobId(string JobId);
        RepositoryResult<Approval> GetApprovals();
    }
}
