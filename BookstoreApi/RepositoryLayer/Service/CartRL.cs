using DatabaseLayer.Cart;
using Microsoft.AspNetCore.Http;
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
    public class CartRL:ICartRL
    {
        private readonly IMongoCollection<Register> _user;
        private readonly IMongoCollection<Book> books;
        private readonly IMongoCollection<Cart> carts;
        private readonly IConfiguration configuration;
       

        public CartRL(IConfig _config, IConfiguration configuration)
        {
            this.configuration = configuration; 
            var cartClient = new MongoClient(_config.ConnectionString);
            var database = cartClient.GetDatabase(_config.DatabaseName);
            _user = database.GetCollection<Register>("_user");
            books = database.GetCollection<Book>("books");
            carts = database.GetCollection<Cart>("carts");
        }

        public async Task<Cart> AddCart(CartPostModel cartPostModel, string userid)
        {
            try
            {
                
                var bookdoc = await books.AsQueryable().Where(x=>x.BookId==cartPostModel.bookId).SingleOrDefaultAsync();
                var userdoc = await _user.AsQueryable().Where(x=>x.UserId==userid).SingleOrDefaultAsync();
                
                Cart cart = new Cart();
                Book book1 = new Book();
                Register register1 = new Register();

                book1.BookId = bookdoc.BookId;
                book1.BookTitle = bookdoc.BookTitle;
                book1.Author = bookdoc.Author;
                book1.Description = bookdoc.Description;
                book1.Rating = bookdoc.Rating;
                book1.totalRating = bookdoc.totalRating;
                book1.DiscountPrice = bookdoc.DiscountPrice;
                book1.ActualPrice = bookdoc.ActualPrice;
                book1.BookImage = bookdoc.BookImage;
                book1.BookQuantity = bookdoc.BookQuantity;

                register1.UserId=userdoc.UserId;
                register1.FullName = userdoc.FullName;
                register1.EmailId=userdoc.EmailId;
                register1.Password = userdoc.Password;
                register1.ContactNumber=userdoc.ContactNumber;

                cart.userId= userid;
                cart.register=register1;
                cart.BookId=cartPostModel.bookId;
                cart.book = book1;
                cart.Quantity = cartPostModel.quantity;

                await carts.InsertOneAsync(cart);
              

                return cart;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteCart(string cartId, string userid)
        {
            try
            {
                var checkcart = carts.AsQueryable().Where(x => x.cartID == cartId && x.userId == userid);
                if(checkcart!=null)
                {
                    await carts.FindOneAndDeleteAsync(x => x.cartID == cartId);
                }
                else
                {
                    throw new Exception("Cart doesn't Exist");
                }

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Cart>> GetCart(string cartId, string userid)
        {
            try
            {
                var checkCart= carts.AsQueryable().Where(x=>x.cartID == cartId && x.userId == userid);
                if(checkCart==null)
                {
                    return null;
                }
                return await carts.Find(_ => true).ToListAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public async Task<List<Cart>> GetAllCart( string userid)
        {
            try
            {
                var checkCart = carts.AsQueryable().Where(x => x.userId == userid);
                if (checkCart == null)
                {
                    return null;
                }
                return await carts.Find(_ => true).ToListAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Cart> UpdateCart(string BookTitle,string Author,int quantity, string userid)
        {
            try
            {

                var cartCheck = await carts.AsQueryable().Where(x => x.book.BookTitle == BookTitle && x.book.Author==Author && x.register.UserId ==userid ).FirstOrDefaultAsync();
                var CartID1 = cartCheck.cartID;
                if (cartCheck != null)
                {
                    await carts.UpdateOneAsync(x => x.cartID == CartID1,
                        Builders<Cart>.Update.Set(x => x.Quantity, quantity));
                    return await carts.AsQueryable().Where(x => x.book.BookTitle == BookTitle && x.book.Author == Author && x.register.UserId == userid).FirstOrDefaultAsync(); ;
                }

                else
                    return null;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
