using DatabaseLogicLayer.Context;
using DatabaseLogicLayer.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogicLayer.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly FunDooContext _context;
        private readonly ILogger<UserRepository> _logger;


        public UserRepository(FunDooContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void Add(User user)
        {
            _logger.LogInformation("Adding user to database: {Email}", user.Email);

            _context.Users.Add(user);
            _context.SaveChanges();

            _logger.LogInformation("User saved successfully with UserId: {UserId}", user.UserId);
        }

        public User GetByEmail(string email)
        {
            _logger.LogDebug("Fetching user by Email: {Email}", email);
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(int userId)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }
        // FORGOT PASSWORD
        public User GetByResetToken(string token)
        {
            return _context.Users.FirstOrDefault(
                u => u.ResetToken == token &&
                     u.ResetTokenExpiry > DateTime.UtcNow
            );
        }
        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

    }
}
