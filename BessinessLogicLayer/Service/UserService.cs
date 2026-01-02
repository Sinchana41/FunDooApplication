using BessinessLogicLayer.Interfaces;
using BessinessLogicLayer.Service;
using DatabaseLogicLayer.Repository.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.DTOs;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserRepository _repo;
        private readonly IConfiguration _config;
        private readonly ILogger<UserService> _logger;
        private readonly EmailService _emailService;

        public UserService(
            UserRepository repo,
            IConfiguration config,
            ILogger<UserService> logger,
            EmailService emailService
        )
        {
            _repo = repo;
            _config = config;
            _logger = logger;
            _emailService = emailService;
        }

        // REGISTER
        public RegisterResponseDto Register(RegisterUserDto dto)
        {
            _logger.LogInformation("Registering user with Email: {Email}", dto.Email);

            var existingUser = _repo.GetByEmail(dto.Email);
            if (existingUser != null)
            {
                _logger.LogWarning("Registration failed. Email already exists: {Email}", dto.Email);
                throw new Exception("Email already exists");
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Password = hashedPassword,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _repo.Add(user);

            _logger.LogInformation("User saved to DB. Sending welcome email to {Email}", user.Email);

            _emailService.Send(
                user.Email,
                "Welcome to FunDoo",
                "You have registered successfully"
            );

            _logger.LogInformation("Welcome email sent to {Email}", user.Email);

            return new RegisterResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Message = "User registered successfully"
            };
        }

        // LOGIN (unchanged)
        public LoginResponseDto Login(LoginUserDto dto)
        {
            var user = _repo.GetByEmail(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new Exception("Invalid credentials");

            string token = GenerateJwtToken(user);
            return new LoginResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Token = token
            };
        }







        // JWT GENERATION
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
            new Claim("UserId", user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public void ForgotPassword(string email)
        {
            var user = _repo.GetByEmail(email);
            if (user == null)
                throw new Exception("User not found");

            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            string resetToken = new JwtSecurityTokenHandler().WriteToken(token);

            //Send via Email (SMTP in next step)
            Console.WriteLine($"Reset Password Token: {resetToken}");
        }

        public void ResetPassword(ResetPasswordDto dto)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            ClaimsPrincipal principal = tokenHandler.ValidateToken(
                dto.Token,
                new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidAudience = _config["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                },
                out _
            );

            int userId = int.Parse(principal.FindFirst("UserId").Value);

            var user = _repo.GetById(userId);
            if (user == null)
                throw new Exception("User not found");

            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            _repo.Update(user);
        }

    }
}
