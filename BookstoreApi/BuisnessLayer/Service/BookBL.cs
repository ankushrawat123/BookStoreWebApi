using BuisnessLayer.Interface;
using DatabaseLayer.Book;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Service
{
    public class BookBL : IBookBL
    {
        public readonly IBookRL bookRL;

        public BookBL(IBookRL bookRL)
        {
          this.bookRL = bookRL;
        }
        public async Task<Book> AddBook(BookPostModel bookPostModel)
        {
            try
            {
               return await this.bookRL.AddBook(bookPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //public async Task UpdateBook( BookPostModel bookPostModel)
        //{
        //    try
        //    {
        //        await this.bookRL.UpdateBook( bookPostModel);
        //    }
        //    catch(Exception e)
        //    {
        //        throw e;
        //    }
        //}

        public async Task DeleteBook(string BookTitle, string Author)
        {
            try
            {
               await this.bookRL.DeleteBook(BookTitle, Author);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            try
            {
                return await this.bookRL.GetAllBook();
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
                return await this.bookRL.GetBook(BookTitle, Author);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateBook(BookPostModel bookPostModel)
        {

            try
            {
                await this.bookRL.UpdateBook(bookPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
