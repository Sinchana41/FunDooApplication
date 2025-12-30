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

       
        public UserController(FunDooContext dBContext, IUserService service, EmailService _emailService)
        {
            this.dBContext = dBContext;
            _service = service;
            this._emailService = _emailService;
                 
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
        public IActionResult Register([FromBody] RegisterUserDto dto)
        {
            if (dto == null)
                return BadRequest("Register data is required");

            _emailService.Send(dto.Email, "Welcomw to Fun Doo Application", "You are registration to Fun Doo App is Done");
        
            var result = _service.Register(dto);
            return Ok(result);
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginUserDto dto)
        {
            if (dto == null)
                return BadRequest("Login data is required");

            var result = _service.Login(dto);
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
