using DatabaseLayer.Book;
using DatabaseLayer.User;
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
    public class WishListRL:IWishListRL
    {
        public readonly IConfiguration configuration;
        public readonly IMongoCollection<WishList> wishlists;
        public readonly IMongoCollection<Book> books;
        public readonly IMongoCollection<Register> _user;
        public WishListRL(IConfiguration configuration,IConfig _config )
        {
            this.configuration=configuration;
            var wishClient = new MongoClient(_config.ConnectionString);
            var Database = wishClient.GetDatabase(_config.DatabaseName);
            wishlists = Database.GetCollection<WishList>("wishlists");
            _user = Database.GetCollection<Register>("_user");
            books = Database.GetCollection<Book>("books");
        }

        public async Task<WishList> AddWishList(string bookid, string userid)
        {
            try
            {
                //var checkuserID =await wishlists.AsQueryable().Where(x => x.UserId == userid).SingleOrDefaultAsync();
                var bookdata = await books.AsQueryable().Where(x => x.BookId == bookid).SingleOrDefaultAsync();
                var userdata = await _user.AsQueryable().Where(x => x.UserId == userid).SingleOrDefaultAsync();
                WishList wishListObj = new WishList();
                Book bookObj = new Book();
                Register registerObj = new Register();

                    bookObj.BookId = bookid;
                    bookObj.BookTitle = bookdata.BookTitle;
                    bookObj.Author = bookdata.Author;
                    bookObj.Description = bookdata.Description;
                    bookObj.Rating = bookdata.Rating;
                    bookObj.totalRating = bookdata.totalRating;
                    bookObj.DiscountPrice = bookdata.DiscountPrice;
                    bookObj.ActualPrice = bookdata.ActualPrice;
                    bookObj.BookImage = bookdata.BookImage;
                    bookObj.BookQuantity = bookdata.BookQuantity;

                    registerObj.UserId = userid;
                    registerObj.FullName = userdata.FullName;
                    registerObj.EmailId = userdata.EmailId;
                    registerObj.Password = userdata.Password;
                    registerObj.ContactNumber = userdata.ContactNumber;

                    wishListObj.book = bookObj;
                    wishListObj.register = registerObj;
                    wishListObj.BookId = bookid;
                    wishListObj.UserId = userid;

                    await wishlists.InsertOneAsync(wishListObj);

                    return  wishListObj;
               
              
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteWishList(string Userid, string wishlistid)
        {
            try
            {
                var checkWishList= await wishlists.AsQueryable().Where(x=>x.UserId==Userid && x.WishListId == wishlistid).SingleOrDefaultAsync();
                if (checkWishList != null)
                {
                    await wishlists.DeleteOneAsync(x => x.WishListId == wishlistid);
                }
               
                    
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<WishList>> GetAllWishList(string userid)
        {
            try
            {
                var checkWishlist = wishlists.AsQueryable().Where(x => x.UserId == userid);
                if (checkWishlist == null)
                {
                    return null;
                }
                return await wishlists.Find(_ => true).ToListAsync();

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<WishList>> GetWishList(string userid, string wishlistid)
        {
            try
            {
               var usercheck = await wishlists.AsQueryable().Where(x => x.UserId == userid&&x.WishListId==wishlistid).SingleOrDefaultAsync();
               if(usercheck==null)
                {
                    return null;
                }
                return await wishlists.Find(x => x.WishListId == wishlistid).ToListAsync();
            
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
