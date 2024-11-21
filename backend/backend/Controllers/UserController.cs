using backend.Dto.LoginsDto;
using backend.Dto.UserDto;
using backend.Interfaces;
using backend.Models;
using backend.Repository;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet("get-users")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUsers()
        {
            var result = userRepository.GetUsers();

            if(!result.Success)
            {
                if(result.Data.Count == 0) return BadRequest(new {success = result.Success, message = result.Message, users = result.Data});

                return BadRequest(new { success = result.Success, message = result.Message });
            }

            return Ok(new { success = result.Success, message = result.Message, users = result.Data });
        }

        [HttpGet("get-single-user/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUserById(string UserId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = userRepository.GetUserById(UserId);

            if (!result.Success)
            {
                if (result.Data.Count == 0) return BadRequest(new { success = result.Success, message = result.Message, users = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message });
            }

            return Ok(new { success = result.Success, message = result.Message, users = result.Data });
        }

        [HttpGet("get-users-job-applications/{JobId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult GetUsersByJobApplication(string JobId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = userRepository.GetUsersByJobApplication(JobId);

            if (!result.Success)
            {
                if (result.Data.Count == 0) return BadRequest(new { success = result.Success, message = result.Message, users = result.Data });

                return BadRequest(new { success = result.Success, message = result.Message });
            }

            return Ok(new { success = result.Success, message = result.Message, users = result.Data });
        }

        [HttpDelete("delete-user/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DeleteUser(string UserId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = userRepository.DeleteUser(UserId);

            if (!result.Success)
            {
                return BadRequest(new { success = result.Success, message = result.Message });
            }

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost("register-user")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] CreateUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            User user = new User()
            {
                UserId = Guid.NewGuid().ToString(),
                Fullname = userDto.Fullname,
                Email = userDto.Email,
                Mobile = userDto.Mobile,
                Country = userDto.Country,
                City = userDto.City,
                ProfilePicture = userDto.ProfilePicture,
                Role = userDto.Role,
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
            };

            var result = userRepository.CreateUser(user);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message , user = result.Data});

            return Ok(new {success =  result.Success, message = result.Message});
        }

        [HttpPost("update-user/{UserId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateUser(string UserId, [FromBody] UpdateUserDto userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            User user = new User()
            {
                UserId = UserId,
                Fullname = userDto.Fullname,
                Email = userDto.Email,
                Mobile = userDto.Mobile,
                Country = userDto.Country,
                City = userDto.City,
                ProfilePicture = userDto.ProfilePicture,
                Role = userDto.Role,
                Password = userDto.Password
            };

            var result = userRepository.UpdateUser(UserId, user);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new { success = result.Success, message = result.Message });
        }

        [HttpPost("login")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult LoginUser([FromBody] LoginDetails loginDetails)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = userRepository.LoginUser(loginDetails);

            if (!result.Success) return BadRequest(new { success = result.Success, message = result.Message });

            return Ok(new { success = result.Success, message = result.Message, user = result.Data });
        }
    }
}
