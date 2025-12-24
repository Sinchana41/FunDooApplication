using BessinessLogicLayer.Interfaces;
using DatabaseLogicLayer.Repository;
using Microsoft.Extensions.Configuration;
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

        public UserService(UserRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        // REGISTER
        public RegisterResponseDto Register(RegisterUserDto dto)
        {
            var existingUser = _repo.GetByEmail(dto.Email);
            if (existingUser != null)
                throw new Exception("Email already exists");

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

            return new RegisterResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Message = "User registered successfully"
            };
        }

        //  LOGIN
        public LoginResponseDto Login(LoginUserDto dto)
        {
            var user = _repo.GetByEmail(dto.Email);
            if (user == null)
                throw new Exception("Invalid credentials");

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!isValid)
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
            new Claim(ClaimTypes.Email, user.Email)
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
    }
}
