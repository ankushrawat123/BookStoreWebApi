using BuisnessLayer.Interface;
using DatabaseLayer.Book;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;

        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Book> books;
      

        public BookController(IBookBL bookBL, IConfig _config, IConfiguration configuration)
        {
            this.bookBL = bookBL;
            this.configuration = configuration;
            var bookclient = new MongoClient(_config.ConnectionString);
            var database = bookclient.GetDatabase(_config.DatabaseName);
            books = database.GetCollection<Book>("books");
        }

        [HttpPost]
        [Route("AddBook")]
        public async Task<IActionResult> AddBook(BookPostModel bookPostModel)
        {
            try
            {
                var book = await this.bookBL.AddBook(bookPostModel);
                if (book != null)
                {
                    return this.Ok(new { Status = true, Message = "Book Added Successfully", Data = book });
                }
                else
                    return this.BadRequest(new { Status = false, Message = "Book Not Added" });
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("GetAllBook")]
        public async Task<IActionResult> GetAllBook()
        {
            try
            {
                List<Book> bookList = new List<Book>();
                bookList = await this.bookBL.GetAllBooks();
                return this.Ok(new { Status = true, Message = "Got All Book Successfully", data = bookList });
            }
            catch (Exception e)
            {
                return NotFound(new { Success = false, Message = e.Message });
            }
        }

        [HttpGet]
        [Route("GetBook/{BookTitle}/{Author}")]

        public async Task<IActionResult> GetBook(string BookTitle, string Author)
        {
            try
            {
                List<Book> bookList = new List<Book>();
                bookList = await this.bookBL.GetBook(BookTitle, Author);
                return this.Ok(new { Status = true, Message = "Got One Book", data = bookList });
            }
            catch (Exception e)
            {
                return NotFound(new { Success = false, Message = e.Message });
            }
        }

        [HttpDelete]
        [Route("DeleteBook/{BookTitle}/{Author}")]

        public async Task<IActionResult> DeleteBook(string BookTitle, string Author)
        {
            try
            {

                var book = this.bookBL.DeleteBook(BookTitle, Author);
                if (book != null)
                {
                    return this.Ok(new { Status = true, Message = "Book Deleted Successfully" });
                }
                else
                    return BadRequest(new { Status = false, Message = "Book Doesn't Exist" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }

        [HttpPut]
        [Route("UpdateBook")]

        public async Task<IActionResult> UpdateBook( BookPostModel bookPostModel)
        {
            try
            {
                var book =  this.bookBL.UpdateBook(bookPostModel);
             
                    if (book != null)
                    {
                        return this.Ok(new { Status = true, Message = "Book Updated Successfully" });
                    }
                    else
                      return BadRequest(new { Status = false, Message = "Book Doesn't Exist" });

            
               
            }
            catch(Exception e)
            {
              throw e;
            }
        }
    }
}
