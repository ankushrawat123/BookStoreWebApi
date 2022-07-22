using DatabaseLayer.User;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly IMongoCollection<Register> _user;
        private readonly IConfiguration configuration;

        public UserRL(IConfig _config, IConfiguration configuration)
        {
            this.configuration = configuration;
            var userclient = new MongoClient(_config.ConnectionString);
            var database = userclient.GetDatabase(_config.DatabaseName);
            _user = database.GetCollection<Register>("_user");
        }

        public async Task<bool> ForgetPassword(string Email)
        {
            try
            {

                var check = await _user.AsQueryable().Where(x => x.EmailId == Email).FirstOrDefaultAsync();
                var userid = check.UserId;
                if (check == null)
                {
                    return false;
                }
                else
                {

                    MessageQueue queue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\BookstoreQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\BookstoreQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\BookstoreQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJwtToken(Email,userid);
                    MyMessage.Label = "Forget Password Email";
                    queue.Send(MyMessage);


                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.SendEmail(Email, msg.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    queue.BeginReceive();
                    queue.Close();


                    return true;
                }
                }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> ResetPassword(string email,PasswordPostModel passwordPostModel)
        {

            try
            {
                var check = await this._user.AsQueryable().Where(x => x.EmailId == email).FirstOrDefaultAsync();

                //var user = fundooContext.Users.Where(u => u.Email == email).FirstOrDefault();

                if (check == null)
                {
                    return false;
                }

                if (passwordPostModel.Password ==passwordPostModel.ConfirmPassword)
                {
                    var cnfpwd = PwdEncryptDecryptService.EncryptPassword(passwordPostModel.ConfirmPassword);
                    await this._user.UpdateOneAsync(x => x.EmailId == passwordPostModel.EmailId,
                       Builders<Register>.Update.Set(x => x.Password, cnfpwd));
                }

                return true;
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
                var userCheck = await this._user.AsQueryable().Where(u => u.EmailId == Email).FirstOrDefaultAsync();
                 var userid = userCheck.UserId;
                if(userCheck!=null)
                {
                    var password = PwdEncryptDecryptService.DecryptPassword(userCheck.Password);
                    if(password == Password)
                    {
                        return GenerateJwtToken(Email,userid);
                    }
                    throw new Exception("Password is Invalid");
                }
                throw new Exception("Email doesn't Exist");

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Register> UserRegister(RegisterPostModel registerPostModel)
        {
            try
            {
                Register _register = new Register();
          
                _register.FullName = registerPostModel.FullName;
                _register.EmailId = registerPostModel.EmailId;
                _register.Password = PwdEncryptDecryptService.EncryptPassword(registerPostModel.Password);
                _register.ContactNumber = registerPostModel.ContactNumber;

                var check = await this._user.AsQueryable().Where(x => x.EmailId == _register.EmailId).SingleOrDefaultAsync();
                if (check == null)
                {
                    await this._user.InsertOneAsync(_register);
                  

                    return _register;
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string GenerateJwtToken(string email, object userId)
        {
            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("this is my secret key");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Email, email),
                    new Claim("UserId", userId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        private string GenerateToken(string Email)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("this is my secret key");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Email,Email)

                    }),
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();

            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }
            }
        }
    }
}



