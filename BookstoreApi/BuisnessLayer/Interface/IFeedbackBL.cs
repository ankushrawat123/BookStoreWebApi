using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface IFeedbackBL
    {

        Task<Feedback> AddFeedback(string userid, string comment, decimal rating, string bookid);

        Task<List<Feedback>> GetAllFeedbacks(string userid);

        Task DeleteFeedback(string FeedbackId, string userid);

        Task<List<Feedback>> GetFeedback(string FeedbackId, string userid);
    }
}
