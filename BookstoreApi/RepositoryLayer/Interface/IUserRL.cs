using DatabaseLayer.User;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        Task<Register> UserRegister(RegisterPostModel registerPostModel);

        Task<string> UserLogin(string Email, string Password);

        Task<Register> ResetPassword(PasswordPostModel passwordPostModel);

        Task<bool> ForgetPassword(string Email);

    }
}
