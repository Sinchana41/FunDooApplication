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
        // AUTH
        RegisterResponseDto Register(RegisterUserDto dto);
        LoginResponseDto Login(LoginUserDto dto);

        // FORGOT PASSWORD
        void ForgotPassword(string email);
        void ResetPassword(ResetPasswordDto dto);

    }
}
