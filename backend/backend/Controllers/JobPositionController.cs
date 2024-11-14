using backend.Dto.JobPositionDto;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPositionController : Controller
    {
        private readonly IJobPositionRepository positionRepository;

        public JobPositionController(IJobPositionRepository positionRepository)
        {
            this.positionRepository = positionRepository;
        }

        [HttpPost("create-position/{JobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePosition(string JobId, [FromBody] CreateJobPositionDto jobPositionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Position position = new Position()
            {
                PositionId = $"{Guid.NewGuid()}",
                JobId = JobId,
                Title = jobPositionDto.Title,
                Price = jobPositionDto.Price,
                RequiredUsers = jobPositionDto.RequiredUsers,
                TimePeriod = jobPositionDto.TimePeriod
            };

            var result = positionRepository.CreatePosition(JobId, position);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new {success = result.Success, message = result.Message});
        }

        [HttpPut("update-position/{PositionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdatePosition(string PositionId, [FromBody] UpdateJobPositionDto jobPositionDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Position position = new Position()
            {
                Title = jobPositionDto.Title,
                Price = jobPositionDto.Price,
                RequiredUsers = jobPositionDto.RequiredUsers,
                TimePeriod = jobPositionDto.TimePeriod
            };

            var result = positionRepository.UpdatePosition(PositionId, position);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("get-position-by-JobId/{JobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPositionsByJobId(string JobId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = positionRepository.GetPositionsByJobId(JobId);

            if (!result.Success)
            {
                if (result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, positions = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message });
            }

            return Ok(new { success = result.Success, message = result.Message, positions = result.Data });
        }

        [HttpDelete("delete-position/{PositionId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeletePosition(string PositionId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = positionRepository.DeletePosition(PositionId);

            if (!result.Success) return BadRequest(new {success = result.Success, message = result.Message});

            return Ok(new {success = result.Success, message = result.  Message});
        }
    }
}
