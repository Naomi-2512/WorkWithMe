using backend.Dto.ApprovalDto;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalController : Controller
    {
        private readonly IApprovalRepository approvalRepository;

        public ApprovalController(IApprovalRepository approvalRepository)
        {
            this.approvalRepository = approvalRepository;
        }

        [HttpPost("create-approval")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateApproval([FromBody] CreateApprovalDto approvalDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var approval = new Approval
            {
                ApprovalId = $"{Guid.NewGuid()}",
                JobId = approvalDto.JobId ,
                OwnerId = approvalDto.OwnerId ,
                ApplierId = approvalDto.ApplierId ,
                PositionApplied = approvalDto.PositionApplied
            };

            var result = approvalRepository.CreateApproval(approval);

            if(!result.Success) return BadRequest(new {success = result.Success, message = result.Message});

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPut("update-approval/{ApprovalId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateApproval(string ApprovalId, [FromBody] UpdateApprovalDto approvalDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Approval approval = new ()
            {
                ApprovalId = approvalDto.ApprovalId,
                JobId = approvalDto.JobId,
                OwnerId = approvalDto.OwnerId,
                ApplierId = approvalDto.ApplierId,
                PositionApplied = approvalDto.PositionApplied
            };

            var result = approvalRepository.UpdateApproval(ApprovalId, approval);

            if (!result.Success) return BadRequest(new {success = result.Success, message = result.Message});

            return Ok(new {success = result.Success, message = result.Message});
        }

        [HttpDelete("delete-approval/{ApprovalId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteApproval(string ApprovalId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = approvalRepository.DeleteApproval(ApprovalId);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("get-all-approvals")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetApprovals()
        {
            var result = approvalRepository.GetApprovals();

            if (!result.Success)
            {
                if (result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, approvals = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message, approvals = result.Data });
            }

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("get-single-approval/{ApprovalId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetApproval(string ApprovalId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = approvalRepository.GetApproval(ApprovalId);

            if (!result.Success)
            {
                if (result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, approvals = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message, approvals = result.Data });
            }

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("get-approval-by-user-Id/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetApprovalByUserId(string UserId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = approvalRepository.GetApprovalByUserId(UserId);

            if (!result.Success)
            {
                if (result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, approvals = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message, approvals = result.Data });
            }

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("get-declined-approval-by-user-Id/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetDeclinedApprovalByUserId(string UserId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = approvalRepository.GetDeclinedApprovalByUserId(UserId);

            if (!result.Success)
            {
                if (result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, approvals = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message, approvals = result.Data });
            }

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpGet("get-approval-by-job-Id/{JobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetApprovalByJobId(string JobId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = approvalRepository.GetApprovalByJobId(JobId);

            if (!result.Success)
            {
                if (result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, approvals = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message, approvals = result.Data });
            }

            return Ok(new { success = result.Success, message = result.Message });
        }
    }
}
