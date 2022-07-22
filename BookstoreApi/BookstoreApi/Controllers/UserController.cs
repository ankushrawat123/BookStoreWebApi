using BuisnessLayer.Interface;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;


        private readonly IMongoCollection<Register> _user;
        private readonly IConfiguration configuration;

     



        public UserController(IUserBL userBL, IConfig _config, IConfiguration configuration)
        {
            this.userBL = userBL;
            this.configuration = configuration;
            var userclient = new MongoClient(_config.ConnectionString);
            var database = userclient.GetDatabase(_config.DatabaseName);
            _user = database.GetCollection<Register>("_user");
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
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userid", StringComparison.InvariantCultureIgnoreCase));
                string UserID = userid.Value;
                var result = _user.AsQueryable().Where(u => u.UserId == UserID).FirstOrDefault();
                string Email = result.EmailId.ToString();
                if (passwordPostModel.Password != passwordPostModel.ConfirmPassword)
                {
                    return BadRequest(new { success = false, message = "Password and ConfirmPassword must be same" });
                }

                bool res = await this.userBL.ResetPassword(Email,passwordPostModel);

                if (res == false)
                {
                    return this.BadRequest(new { success = false, message = "Enter the valid Email" });
                }
                return this.Ok(new { success = true, message = "Password Update Successfull" });



                //var user = await this.userBL.ResetPassword(passwordPostModel);
                //if(user != null)
                //{
                //    return this.Ok(new { status=true,Message="Password Reset Successfull", });
                //}
                //else
                //{
                //    return BadRequest(new { status = false, Message = "Email Is Not Registered, Please Registered It" });
                //}
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
