using BessinessLogicLayer.Implementations;
using BessinessLogicLayer.Interfaces;
using BessinessLogicLayer.Service;
using DatabaseLogicLayer.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using System.Security.Claims;

namespace FunDooAPP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FunDooContext dBContext;
        private readonly IUserService _service;
        private readonly EmailService _emailService;
        private readonly ILogger<UserController> _logger;


        public UserController(FunDooContext dBContext, IUserService service, EmailService _emailService, ILogger<UserController> logger)
        {
            this.dBContext = dBContext;
            _service = service;
            this._emailService = _emailService;
            _logger = logger;

        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var allUsers = dBContext.Users.ToList();

            return Ok(allUsers);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetUsersById(int id)
        {
            var user = dBContext.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }

        [HttpPost]
        public IActionResult AddUser(UserRequestDTO userRequest)
        {
            var userEntity = new User()
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                Password = userRequest.Password
            };

            dBContext.Users.Add(userEntity);
            dBContext.SaveChanges();

            return Ok(userEntity);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateUserById(int id, UpdateUserRequestDTO updateUserRequest)
        {
            var user = dBContext.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            user.FirstName = updateUserRequest.FirstName;
            user.LastName = updateUserRequest.LastName;
            user.Email = updateUserRequest.Email;
            user.Password = updateUserRequest.Password;

            dBContext.SaveChanges();
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteEmployeeById(int id)
        {
            var user = dBContext.Users.Find(id);
            dBContext.Remove(user);
            dBContext.SaveChanges();
            return Ok(user);
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterUserDto dto)
        {
            _logger.LogInformation("Register request received for Email: {Email}", dto.Email);

            var result = _service.Register(dto);

            _logger.LogInformation("User registered successfully with UserId: {UserId}", result.UserId);

            return Ok(result);
        }


        [HttpPost("login")]
        public IActionResult Login(LoginUserDto dto)
        {
            _logger.LogInformation("Login attempt for Email: {Email}", dto.Email);

            var result = _service.Login(dto);

            _logger.LogInformation("Login successful for UserId: {UserId}", result.UserId);

            return Ok(result);
        }

        [HttpPost("forgot-password")]
        public IActionResult ForgotPassword(ForgotPasswordDto dto)
        {
            _service.ForgotPassword(dto.Email);
            return Ok("Reset link sent to email");
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword([FromBody] ResetPasswordDto dto)
        {
            _service.ResetPassword(dto);
            return Ok("Password reset successful");
        }

    }
}
