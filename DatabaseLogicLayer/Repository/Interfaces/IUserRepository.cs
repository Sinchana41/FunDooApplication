using ModelLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseLogicLayer.Repository.Interfaces
{
    public interface IUserRepository
    {
        void Add(User user);

        User GetByEmail(string email);
        User GetById(int userId);
        IEnumerable<User> GetAll();

        // FORGOT PASSWORD SUPPORT
        User GetByResetToken(string token);
        
        void Update(User user);
        void Delete(User user);
    }
}