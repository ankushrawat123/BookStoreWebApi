using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    public class FeedbackRL:IFeedbackRL
    {

        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Book> books;
        private readonly IMongoCollection<Register> _user;
        private readonly IMongoCollection<Feedback> feedbacks;
        public FeedbackRL(IConfiguration configuration, IConfig _config)
        {
            this.configuration = configuration;
            var orderClient = new MongoClient(_config.ConnectionString);
            var database = orderClient.GetDatabase(_config.DatabaseName);
            feedbacks = database.GetCollection<Feedback>("feedbacks");
            books = database.GetCollection<Book>("books");
            _user = database.GetCollection<Register>("_user");
      
        }

        public async Task<Feedback> AddFeedback(string userid, string comment, decimal rating,string bookid)
        {
            try
            {
               
                var userData = await _user.AsQueryable().Where(x => x.UserId == userid).FirstOrDefaultAsync();
                var bookData = await books.AsQueryable().Where(x => x.BookId == bookid).FirstOrDefaultAsync();

                Feedback feedbackObj = new Feedback();
                Register registerObj = new Register();
                Book bookObj = new Book();

                registerObj.UserId = userid;
                registerObj.FullName = userData.FullName;
                registerObj.EmailId = userData.EmailId;
                registerObj.Password = userData.Password;
                registerObj.ContactNumber = userData.ContactNumber;



                bookObj.BookId = bookData.BookId;
                bookObj.BookTitle = bookData.BookTitle;
                bookObj.Author = bookData.Author;
                bookObj.Description = bookData.Description;
                bookObj.Rating = bookData.Rating;
                bookObj.totalRating = bookData.totalRating;
                bookObj.DiscountPrice = bookData.DiscountPrice;
                bookObj.ActualPrice = bookData.ActualPrice;
                bookObj.BookImage = bookData.BookImage;
                bookObj.BookQuantity = bookData.BookQuantity;


                feedbackObj.BookId = bookData.BookId;
                feedbackObj.book = bookObj;
                feedbackObj.userId = userid;
                feedbackObj.register = registerObj;
                feedbackObj.Comment = comment;
                feedbackObj.Rating = rating;
         
                await feedbacks.InsertOneAsync(feedbackObj);
                return feedbackObj;
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
                var checkfeedback = feedbacks.AsQueryable().Where(x => x.userId == userid);
                if (checkfeedback == null)
                {
                    return null;
                }
                return await feedbacks.Find(_ => true).ToListAsync();
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
                var checkfeedback = feedbacks.AsQueryable().Where(x => x.feedbackID == FeedbackId && x.userId == userid);
                if (checkfeedback != null)
                {
                    await feedbacks.FindOneAndDeleteAsync(x => x.feedbackID== FeedbackId);
                }
                else
                {
                    throw new Exception("Feedback doesn't Exist");
                }

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
                var checkFeedback = feedbacks.AsQueryable().Where(x => x.feedbackID == FeedbackId && x.userId == userid);
                if (checkFeedback == null)
                {
                    return null;
                }
                return await feedbacks.Find(x => x.feedbackID == FeedbackId).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

     


       
        
    }
}
