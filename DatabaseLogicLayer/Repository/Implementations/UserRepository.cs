using DatabaseLogicLayer.Context;
using DatabaseLogicLayer.Repository.Interfaces;
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

        public UserRepository(FunDooContext context)
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
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
