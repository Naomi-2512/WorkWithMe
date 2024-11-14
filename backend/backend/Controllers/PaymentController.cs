using backend.Dto.PaymentDto;
using backend.Interfaces;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            this.paymentRepository = paymentRepository;
        }

        [HttpPost("make-payment/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult MakePayment(string UserId, [FromBody] MakePaymentDto paymentDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            Payment payment = new Payment()
            {
                PaymentId = $"{Guid.NewGuid()}",
                JobId = paymentDto.JobId ,
                UserId = UserId ,
                Price = paymentDto.Price ,
                AdditionalPrice = paymentDto.AdditionalPrice
            };

            var result = paymentRepository.MakePayment(UserId, payment);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new {success = result.Success, message = result.Message});
        }

        [HttpGet("get-payments-history")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPayments()
        {
            var result = paymentRepository.GetPayments();

            if(!result.Success)
            {
                if (result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, payments = result.Data });

                return BadRequest(new {success = result.Success, message = result.Message});
            }

            return Ok(new { success = result.Success, message = result.Message, payments = result.Data });
        }

        [HttpGet("get-payments-by-manager/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetPaymentsByUserId(string UserId)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var result = paymentRepository.GetPaymentsByUserId(UserId);

            if (!result.Success)
            {
                if (result.Data.Count == 0) return NotFound(new { success = result.Success, message = result.Message, payments = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message });
            }

            return Ok(new { success = result.Success, message = result.Message, payments = result.Data });
        }
    }
}
