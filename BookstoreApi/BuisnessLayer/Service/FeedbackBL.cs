using BuisnessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Service
{
  
    public class FeedbackBL:IFeedbackBL
    {
        private readonly IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public async Task<Feedback> AddFeedback(string userid, string comment, decimal rating, string bookid)
        {
                try
                {
                    return await feedbackRL.AddFeedback(userid,comment,rating,bookid);
                }
                catch (Exception e)
                {
                    throw e;
                }
        }
        public async Task<List<Feedback>> GetAllFeedbacks(string userid)
        {
            try
            {
                return await feedbackRL.GetAllFeedbacks(userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteFeedback(string FeedbackId, string userid)
        {
            try
            {

                await feedbackRL.DeleteFeedback(FeedbackId, userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Feedback>> GetFeedback(string FeedbackId, string userid)
        {
            try
            {
                return await feedbackRL.GetFeedback(FeedbackId, userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
