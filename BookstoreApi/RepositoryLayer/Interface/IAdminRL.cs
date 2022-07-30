using DatabaseLayer.Admin;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAdminRL
    {

        Task<Admin> AdminRegister(AdminPostModel adminPostModel);

        Task<string> AdminLogin(string Email, string Password);


        Task<bool> ResetPassword(string email, AdminPasswordPostModel adminPasswordPostModel);

        Task<bool> ForgetPassword(string Email);
    }
}
