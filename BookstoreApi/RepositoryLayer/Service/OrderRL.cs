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
    public class OrderRL:IOrderRL
    {
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Order> orders;
        private readonly IMongoCollection<Book> books;
        private readonly IMongoCollection<Register> _user;
        private readonly IMongoCollection<Address> addresses;
        public OrderRL(IConfiguration configuration,IConfig _config)
        {
            this.configuration = configuration;
            var orderClient = new MongoClient(_config.ConnectionString);
            var database = orderClient.GetDatabase(_config.DatabaseName);
            orders= database.GetCollection<Order>("orders");
            books = database.GetCollection<Book>("books");
            _user = database.GetCollection<Register>("_user");
            addresses = database.GetCollection<Address>("addresses");
        }

        public async Task<Order> AddOrder(string userid, OrderPostModel orderPostModel)
        {
            try
            {
                var orderData = await orders.AsQueryable().Where(x => x.UserId == userid).FirstOrDefaultAsync();
                var userData = await _user.AsQueryable().Where(x => x.UserId == userid).FirstOrDefaultAsync();
                var addressData = await addresses.AsQueryable().Where(x => x.UserId == userid).FirstOrDefaultAsync();
                var bookData = await books.AsQueryable().Where(x => x.BookTitle == orderPostModel.BookTitle && x.Author == orderPostModel.Author).FirstOrDefaultAsync();

                Order orderObj = new Order();
                Address addressObj = new Address();
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

                addressObj.AddressId= addressData.AddressId;
                addressObj.UserId = userid;
                addressObj.register = registerObj;
                addressObj.addressTypeId = orderPostModel.addressTypeId;
                addressObj.Addresses = orderPostModel.Addresses;
                addressObj.City = orderPostModel.City;
                addressObj.State = orderPostModel.State;
                addressObj.Pincode = orderPostModel.Pincode;

                addressObj.register = registerObj;




                orderObj.BookId= bookData.BookId;
                orderObj.book=bookObj;
                orderObj.UserId= userid;
                orderObj.register = registerObj;
                orderObj.AddressId=addressData.AddressId;
                orderObj.address=addressObj;
                orderObj.Quantity= orderPostModel.Quantity;
                orderObj.Price= orderPostModel.Price;
                await orders.InsertOneAsync(orderObj);
                return orderObj;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Order>> GetAllOrders(string userid)
        {
            try
            {
                var checkCart = orders.AsQueryable().Where(x => x.UserId == userid);
                if (checkCart == null)
                {
                    return null;
                }
                return await orders.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task DeleteOrder(string orderId, string userid)
        {
            try
            {
                var checkorder = orders.AsQueryable().Where(x => x.orderId == orderId && x.UserId == userid);
                if (checkorder != null)
                {
                    await orders.FindOneAndDeleteAsync(x => x.orderId== orderId);
                }
                else
                {
                    throw new Exception("Order doesn't Exist");
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<List<Order>> GetOrder(string orderId, string userid)
        {
            try
            {
                var checkOrder = orders.AsQueryable().Where(x => x.orderId== orderId && x.UserId == userid);
                if (checkOrder == null)
                {
                    return null;
                }
                return await orders.Find(x => x.orderId == orderId).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
