using backend.Data;
using backend.Interfaces;
using backend.Models;
using backend.RepositoryResults;

namespace backend.Repository
{
    public class JobPositionRepository : IJobPositionRepository
    {
        private readonly DataContext dataContext;

        public JobPositionRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public bool Save() => dataContext.SaveChanges() > 1;

        public RepositoryResult<Position> CreatePosition(string JobId, Position position)
        {
            if (string.IsNullOrWhiteSpace(JobId)) return new RepositoryResult<Position>(false, "Job identification provided is invalid.", new List<Position>());

            Job? job = dataContext.Jobs.FirstOrDefault(job => job.JobId == JobId);

            if (job == null) return new RepositoryResult<Position>(false, "Positions unavailable because Job is not found.", new List<Position>());

            dataContext.Positions.Add(position);

            if (!Save()) return new RepositoryResult<Position>(false, "Unable to create position at the moment.", new List<Position>());

            return new RepositoryResult<Position>(true, "Position created successfully", new List<Position>());
        }

        public RepositoryResult<Position> UpdatePosition(string PositionId, Position position)
        {
            if (string.IsNullOrWhiteSpace(PositionId)) return new RepositoryResult<Position>(false, "Position identification provided is invalid.", new List<Position>());

            Position? _position = dataContext.Positions.FirstOrDefault(position => position.PositionId == PositionId);

            if (_position == null) return new RepositoryResult<Position>(false, "The position specified is not found.", new List<Position>());

            _position.PositionId = PositionId;
            _position.Price = position.Price;
            _position.Title = position.Title;
            _position.RequiredUsers = position.RequiredUsers;
            _position.TimePeriod = position.TimePeriod;

            if (!Save()) return new RepositoryResult<Position>(false, "Unable to update position at the moment.", new List<Position>());

            return new RepositoryResult<Position>(true, "Position updated successfully", new List<Position>());
        }

        public RepositoryResult<Position> GetPositionsByJobId(string JobId)
        {
            if (string.IsNullOrWhiteSpace(JobId)) return new RepositoryResult<Position>(false, "Job identification provided is invalid", new List<Position>());

            Job? job = dataContext.Jobs.FirstOrDefault(job => job.JobId == JobId);

            if (job == null) return new RepositoryResult<Position>(false, "Positions unavailable because Job is not found.", new List<Position>());

            List<Position> positions = dataContext.Positions.Where(position => position.JobId == JobId).OrderBy(position => position.PositionId).ToList();

            if (positions == null) return new RepositoryResult<Position>(false, "Unable to fetch positions at the moment.", new List<Position>());

            if (positions.Count == 0) return new RepositoryResult<Position>(false, "No positions currently unavailable for this job", positions);

            return new RepositoryResult<Position>(true, "Positions fetched successfully", positions);
        }

        public RepositoryResult<Position> DeletePosition(string PositionId)
        {
            if (string.IsNullOrWhiteSpace(PositionId)) return new RepositoryResult<Position>(false, "Position identification provided is invalid.", new List<Position>());

            Position? _position = dataContext.Positions.FirstOrDefault(position => position.PositionId == PositionId);

            if (_position == null) return new RepositoryResult<Position>(false, "The position specified is not found.", new List<Position>());

            dataContext.Positions.Remove(_position);

            if (!Save()) return new RepositoryResult<Position>(false, $"Unable to delete position at the moment", new List<Position>());

            return new RepositoryResult<Position>(true, "Position deleted successfully", _position);
        }
    }
}
