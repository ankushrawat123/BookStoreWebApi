using BuisnessLayer.Interface;
using DatabaseLayer.Admin;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Service
{
    public class AdminBL:IAdminBL
    {
        IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public async Task<bool> ForgetPassword(string Email)
        {
            try
            {
                return await this.adminRL.ForgetPassword(Email);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<bool> ResetPassword(string email, AdminPasswordPostModel adminPasswordPostModel)
        {
            try
            {
                return await this.adminRL.ResetPassword(email, adminPasswordPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> AdminLogin(string Email, string Password)
        {
            try
            {
                return await this.adminRL.AdminLogin(Email, Password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Admin> AdminRegister(AdminPostModel adminPostModel)
        {
            try
            {
                return await this.adminRL.AdminRegister(adminPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
