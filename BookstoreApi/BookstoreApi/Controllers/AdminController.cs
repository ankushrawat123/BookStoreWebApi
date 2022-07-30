using BuisnessLayer.Interface;
using DatabaseLayer.Admin;
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
        public class AdminController : Controller
        {
            private readonly IAdminBL adminBL;


            private readonly IMongoCollection<Admin> admin;
            private readonly IConfiguration configuration;
            public AdminController(IAdminBL userBL, IConfig _config, IConfiguration configuration)
            {
                this.adminBL = userBL;
                this.configuration = configuration;
                var adminclient = new MongoClient(_config.ConnectionString);
                var database = adminclient.GetDatabase(_config.DatabaseName);
                admin = database.GetCollection<Admin>("admin");
            }

            [HttpPost]
            [Route("Adminregister")]

            public async Task<IActionResult> RegisterUser(AdminPostModel adminPostModel)
            {
                try
                {

                    var register = await this.adminBL.AdminRegister(adminPostModel);
                    if (register != null)
                    {
                        return this.Ok(new ResponseModel<AdminPostModel> { Status = true, Message = "Admin Registered Successfully", Data = register });
                    }
                    else
                    {
                        return this.BadRequest(new { Status = false, Message = "Admin Not Registered" });
                    }
                }

                catch (Exception e)
                {
                    return this.NotFound(new { Status = false, Message = e.Message });
                }
            }


            [HttpPost]
            [Route("AdminLogin/{email}/{password}")]

            public async Task<IActionResult> AdminLogin(string email, string password)
            {
                try
                {

                    var login = await this.adminBL.AdminLogin(email, password);
                    if (login != null)
                    {
                        var token = login;
                        return this.Ok(new { Status = true, Message = "AdminLogin Is Successfull", Token = token });
                    }

                    else
                    {
                        return this.BadRequest(new { Status = false, Message = "Admin User doesn't exist" });
                    }

                }
                catch (Exception e)
                {
                    return this.NotFound(new { status = false, Message = e.Message });
                }
            }

            [Authorize]
            [HttpPut("ResetPassword")]
            public async Task<IActionResult> ResetPassword(AdminPasswordPostModel adminPasswordPostModel)
            {
                try
                {
                    var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                    string UserID = userid.Value;
                    var result =  admin.AsQueryable().Where(u => u.UserId == UserID).FirstOrDefault();
                    string Email = result.EmailId.ToString();
                    if (adminPasswordPostModel.Password != adminPasswordPostModel.ConfirmPassword)
                    {
                        return BadRequest(new { success = false, message = "Password and ConfirmPassword must be same" });
                    }

                    bool res = await this.adminBL.ResetPassword(Email, adminPasswordPostModel);

                    if (res == false)
                    {
                        return this.BadRequest(new { success = false, message = "Enter the valid Email" });
                    }
                    return this.Ok(new { success = true, message = "Password Update Successfull" });
  
                }
                catch (Exception e)
                {
                    return this.NotFound(new { status = false, Message = e.Message });
                }
            }


            [HttpPost("ForgetPassword/{email}")]

            public async Task<IActionResult> ForgetPassword(string email)
            {
                try
                {
                    var user = await this.adminBL.ForgetPassword(email);
                    if (user == true)
                    {
                        return this.Ok(new { status = true, Message = "Forgot Password" });
                    }
                    else
                    {
                        return BadRequest(new { status = false, Message = "Email Doesn't Registered, Please Registered it" });
                    }
                }
                catch (Exception e)
                {
                    return NotFound(new { status = false, Message = e.Message });
                }
            }


        }
    
}
