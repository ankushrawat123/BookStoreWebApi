using BuisnessLayer.Interface;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Service.Entity;
using System;
using System.Threading.Tasks;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
      
        [HttpPost]
        [Route("register")]

        public async Task<IActionResult> RegisterUser(RegisterPostModel registerPostModel)
        {
            try
            {

                var register= await this.userBL.UserRegister(registerPostModel);
                if(register != null)
                {
                    return this.Ok(new ResponseModel<RegisterPostModel> { Status = true, Message = "User Registered Successfully",Data=register });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "User Not Registered" });
                }
            }

            catch(Exception e)
            {
                return this.NotFound(new {Status=false,Message=e.Message});
            }
        }


        [HttpPost]
        [Route("Login/{email}/{password}")]

        public async Task<IActionResult> Login(string email,string password)
        {
            try
            {

                var login = await this.userBL.UserLogin(email, password);
                if(login != null)
                {
                    var token = login;
                    return this.Ok(new { Status = true, Message = "login Is Successfull",Token=token });
                }

                else
                {
                    return  this.BadRequest(new { Status = false, Message = "User doesn't exist" });
                }         

            }
            catch(Exception e)
            {
                return this.NotFound(new {status=false, Message=e.Message});
            }
        }

        [Authorize]
        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(PasswordPostModel passwordPostModel)
        {
            try
            {
                var user = await this.userBL.ResetPassword(passwordPostModel);
                if(user != null)
                {
                    return this.Ok(new { status=true,Message="Password Reset Successfull", });
                }
                else
                {
                    return BadRequest(new { status = false, Message = "Email Is Not Registered, Please Registered It" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new {status=false,Message=e.Message});
            }
        }

      
        [HttpPost("ForgetPassword/{email}")]

        public async Task<IActionResult> ForgetPassword(string email)
        {
            try
            {
                var user = await this.userBL.ForgetPassword(email);
                if (user == true)
                {
                    return this.Ok(new { status = true, Message = "Forgot Password" });
                }
                else
                {
                    return BadRequest(new { status = false, Message = "Email Doesn't Registered, Please Registered it" });
                }
            }
            catch(Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }


    }
}
