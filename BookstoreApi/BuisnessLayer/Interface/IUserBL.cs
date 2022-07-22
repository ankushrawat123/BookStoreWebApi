using DatabaseLayer.User;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface IUserBL
    {
        Task<Register> UserRegister(RegisterPostModel registerPostModel);

        Task<string> UserLogin(string Email, string Password);

        Task<Register> ResetPassword(PasswordPostModel passwordPostModel);

        Task<bool> ForgetPassword(string Email);
    }
}
