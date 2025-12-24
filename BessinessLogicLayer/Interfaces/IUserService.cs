using ModelLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BessinessLogicLayer.Interfaces
{
    public interface IUserService
    {
        public RegisterResponseDto Register(RegisterUserDto dto);
       public LoginResponseDto Login(LoginUserDto dto);

    }
}
