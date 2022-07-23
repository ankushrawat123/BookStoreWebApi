using DatabaseLayer.Book;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBookRL
    {
        Task<Book> AddBook(BookPostModel bookPostModel);
        Task<List<Book>> GetAllBook();

        Task<List<Book>> GetBook(string BookTitle, string Author);

        Task DeleteBook(string BookTitle, string Author);

        Task UpdateBook(BookPostModel bookPostModel);
    }
}
