using BuisnessLayer.Interface;
using DatabaseLayer.User;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Service
{
    public class UserBL : IUserBL
    {
         IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public async Task<bool> ForgetPassword(string Email)
        {
            try
            {
                return await this.userRL.ForgetPassword(Email); 
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Register> ResetPassword(PasswordPostModel passwordPostModel)
        {
            try
            {
                return await this.userRL.ResetPassword(passwordPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<string> UserLogin(string Email, string Password)
        {
            try
            {
                return await this.userRL.UserLogin(Email, Password);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<Register> UserRegister(RegisterPostModel registerPostModel)
        {
            try
            {
                return await this.userRL.UserRegister(registerPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
