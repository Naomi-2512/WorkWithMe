using backend.Dto.JobDto;
using backend.Interfaces;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {
        private readonly IJobRepository jobRepository;

        public JobController(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        [HttpGet("all-jobs")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetJobs()
        {
            var jobs = jobRepository.GetJobs();

            if (jobs.Success == false) return BadRequest(new { sucess = jobs.Success, message = jobs.Message });

            if (jobs.Data == null || jobs.Data.Count == 0) return NotFound(new { success = jobs.Success, message = jobs.Message, jobs = jobs.Data });

            return Ok(new { success = jobs.Success, message = jobs.Message, jobs = jobs.Data });
        }

        [HttpGet("Activated-Jobs")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetActivatedJobs()
        {
            var jobs = jobRepository.GetActivatedJobs();

            if (jobs.Success == false)
            {
                return BadRequest(new { success = jobs.Success, message = jobs.Message });
            }

            if (jobs.Data == null || jobs.Data.Count == 0) return NotFound(new { success = jobs.Success, message = jobs.Message, jobs = jobs.Data });

            return Ok(new { success = jobs.Success, message = jobs.Message, jobs = jobs.Data });
        }

        [HttpPost("create-job/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateJob(string UserId, [FromBody] CreateJobDto jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var job = new Job
            {
                JobId = $"{Guid.NewGuid()}",
                UserId = UserId,
                Title = jobDto.Title,
                CompanyName = jobDto.CompanyName,
                JobPositions = jobDto.Position,
                Category = jobDto.Category,
                Country = jobDto.Country,
                City = jobDto.City,
                Description = jobDto.Description
            };

            var result = jobRepository.CreateJob(UserId, job);

            if (result.Success == false) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPut("update-job/{JobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateJob(string JobId, UpdateJobDto jobDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Job job = new Job
            {
                Title = jobDto.Title,
                CompanyName = jobDto.CompanyName,
                JobPositions = jobDto.Position,
                Category = jobDto.Category,
                Country = jobDto.Country,
                City = jobDto.City,
                Description = jobDto.Description
            };

            var result = jobRepository.UpdateJob(JobId, job);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpDelete("delete-job/{JobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteJob(string JobId)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = jobRepository.DeleteJob(JobId);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("get-single-job/{JobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetJobById(string JobId)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = jobRepository.GetJobById(JobId);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            if (result.Data == null || result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, jobs = result.Data });

            return Ok(new { success = result.Success, message = result.Message, jobs = result.Data });
        }

        [HttpGet("user-jobs/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetJobByUserId(string UserId)
        {
            if (!ModelState.IsValid) { return BadRequest(ModelState); }

            var result = jobRepository.GetJobByUserId(UserId);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            if (result.Data == null || result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, jobs = result.Data });

            return Ok(new { success = result.Success, message = result.Message, jobs = result.Data });
        }

        [HttpGet("update/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult UpdateJobStatus(string UserId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = jobRepository.UpdateJobStatus(UserId);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new { success = result.Success, message = result.Message });
        }

    }
}
