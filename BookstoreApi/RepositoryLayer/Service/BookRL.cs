using DatabaseLayer.Book;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
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
    public class BookRL: IBookRL
    {
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Book> books;
        public BookRL(IConfig _config, IConfiguration configuration)
        {
            this.configuration = configuration;
            var bookclient = new MongoClient(_config.ConnectionString);
            var database = bookclient.GetDatabase(_config.DatabaseName);
            books = database.GetCollection<Book>("books");
        }

        public async Task<Book> AddBook(BookPostModel bookPostModel)
        {
            try
            {
                Book book = new Book();
                var check = books.AsQueryable().Where(x => x.BookId == book.BookId).SingleOrDefault();
                if (check == null)
                {
                    book.BookTitle = bookPostModel.BookTitle;
                    book.Author = bookPostModel.Author;
                    book.Description = bookPostModel.Description;
                    book.Rating = bookPostModel.Rating;
                    book.totalRating = bookPostModel.totalRating;
                    book.DiscountPrice = bookPostModel.DiscountPrice;
                    book.ActualPrice = bookPostModel.ActualPrice;
                    book.BookImage = bookPostModel.BookImage;
                    book.BookQuantity = bookPostModel.BookQuantity;
                    await this.books.InsertOneAsync(book);
               
                    return book;
                }
                else
                {
                   return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteBook(string BookTitle, string Author)
        {
            try
            {
                var book = await books.AsQueryable().Where(x => x.BookTitle == BookTitle && x.Author == Author).SingleOrDefaultAsync();
                if (book != null)
               await books.FindOneAndDeleteAsync(x => x.BookTitle == BookTitle && x.Author == Author);
             
                    
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Book>> GetAllBook()
        {
            try
            {
                    return await books.Find(_ => true).ToListAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Book>> GetBook(string BookTitle, string Author)
        {
            try
            {
               
                return await books.Find(x => x.BookTitle == BookTitle && x.Author == Author).ToListAsync();

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateBook( BookPostModel bookPostModel)
        {
            try
            {

                var bookCheck = await books.AsQueryable().Where(x => x.BookTitle==bookPostModel.BookTitle && x.Author==bookPostModel.Author).SingleOrDefaultAsync();
                var bookId = bookCheck.BookId;
                if (bookId != null)
                {
                           await books.UpdateOneAsync(x => x.BookId == bookId,
                            Builders<Book>.Update.Set(x => x.BookTitle, bookPostModel.BookTitle)
                                 .Set(x => x.Author, bookPostModel.Author)
                                 .Set(x => x.Description, bookPostModel.Description)
                                 .Set(x => x.Rating, bookPostModel.Rating)
                                 .Set(x => x.totalRating, bookPostModel.totalRating)
                                 .Set(x => x.DiscountPrice, bookPostModel.DiscountPrice)
                                 .Set(x => x.ActualPrice, bookPostModel.ActualPrice)
                                 .Set(x => x.BookImage, bookPostModel.BookImage)
                                 .Set(x => x.BookQuantity, bookPostModel.BookQuantity));
                    
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

      
    }
}


//public async Task UpdateBook(string BookId, BookPostModel bookPostModel)
//{
//    try
//    {
//        var bookCheck = books.AsQueryable().Where(x => x.BookId == BookId).SingleOrDefaultAsync();
//        if (bookCheck != null)
//        {
//            await this.books.UpdateOneAsync(x => x.BookId == BookId,
//             Builders<BookPostModel>.Update.Set(x => x.BookTitle, bookPostModel.BookTitle)
//                  .Set(x => x.Author, bookPostModel.Author)
//                  .Set(x => x.Description, bookPostModel.Description)
//                  .Set(x => x.Rating, bookPostModel.Rating)
//                  .Set(x => x.totalRating, bookPostModel.totalRating)
//                  .Set(x => x.DiscountPrice, bookPostModel.DiscountPrice)
//                  .Set(x => x.ActualPrice, bookPostModel.ActualPrice)
//                  .Set(x => x.BookImage, bookPostModel.BookImage)
//                  .Set(x => x.BookQuantity, bookPostModel.BookQuantity));

//        }


//        await this.books.UpdateOneAsync(x => x.BookId == book.BookId,
//             Builders<BookModel>.Update.Set(x => x.BookName, book.BookName)
//             .Set(x => x.Description, book.Description)
//             .Set(x => x.AuthorName, book.AuthorName)
//             .Set(x => x.Rating, book.Rating)
//             .Set(x => x.totalRating, book.totalRating)
//             .Set(x => x.DiscountPrice, book.DiscountPrice));


//    }
//    catch (Exception e)
//    {
//        throw e;
//    }