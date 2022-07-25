using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApi.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedbackBL feedbackBL;
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Feedback> feedbacks;
        public FeedbackController(IFeedbackBL feedbackBL, IConfiguration configuration, IConfig _config)
        {
            this.feedbackBL = feedbackBL;
            this.configuration = configuration;
            var addressClient = new MongoClient(_config.ConnectionString);
            var database = addressClient.GetDatabase(_config.DatabaseName);
            feedbacks = database.GetCollection<Feedback>("feedbacks");
        }


        [Authorize]
        [HttpPost]
        [Route("AddFeedBack/{comment}/{rating}/{bookid}")]

        public async Task<IActionResult> AddFeedback(string comment,decimal rating, string bookid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = userid.Value;


                if (UserID != null)
                {
                    var feedbackData = feedbackBL.AddFeedback(UserID,comment, rating,bookid);
                    return Ok(new { success = true, Message = "Feedback Submitted Successfully", data = feedbackData });
                }
                return BadRequest(new { status = false, Message = "Feedback Not Submitted" });

            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllFeedbacks")]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = userid.Value;
                if (UserID != null)
                {
                    List<Feedback> FeedbackList = new List<Feedback>();
                    FeedbackList = await feedbackBL.GetAllFeedbacks(UserID);
                    return Ok(new { status = true, Message = "All Feedbacks Obtained Successfully!", data = FeedbackList });
                }
                return BadRequest(new { status = false, Message = "Feedback Not Obtained " });
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, message = e.Message });
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("DeleteFeedback /{feedbackid}")]
        public async Task<IActionResult> DeleteFeedback(string feedbackid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string userId = userid.Value;
                if (userid != null)
                {
                    await feedbackBL.DeleteFeedback(feedbackid, userId);
                    return Ok(new { Status = true, Message = "Feedback Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "No Feedback found with this Id" });
                }
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }


        [Authorize]
        [HttpGet]
        [Route("GetFeedback/{feedbackid}")]

        public async Task<IActionResult> GetFeedback(string feedbackid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string userId = userid.Value;
                if (userId != null)
                {
                    List<Feedback> FeedbackList = new List<Feedback>();
                    FeedbackList = await feedbackBL.GetFeedback(feedbackid, userId);
                    return Ok(new { Status = true, Message = "Got One Feedback Successfully", data = FeedbackList });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "feedbackId Not Exist" });
                }

            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }


    }
}
