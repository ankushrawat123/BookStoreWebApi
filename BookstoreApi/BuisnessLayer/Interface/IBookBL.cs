using DatabaseLayer.Book;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface IBookBL
    {
        Task<Book> AddBook(BookPostModel bookPostModel);
        Task<List<Book>> GetAllBooks();
        Task<List<Book>> GetBook(string BookTitle, string Author);

        Task DeleteBook(string BookTitle, string Author);
        Task UpdateBook( BookPostModel bookPostModel);
    }
}
