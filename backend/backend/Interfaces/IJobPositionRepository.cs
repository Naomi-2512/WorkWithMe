using backend.Models;
using backend.RepositoryResults;

namespace backend.Interfaces
{
    public interface IJobPositionRepository
    {
        bool Save();
        RepositoryResult<Position> CreatePosition(string JobId, Position position);
        RepositoryResult<Position> UpdatePosition(string PositionID, Position position);
        RepositoryResult<Position> DeletePosition(string PositionID);
        RepositoryResult<Position> GetPositionsByJobId(string JobId);
    }
}
