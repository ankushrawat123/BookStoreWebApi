using DatabaseLayer.Cart;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        Task<Cart> AddCart(CartPostModel cartPostModel, string userid);
        Task DeleteCart(string cartId,string userid);
        Task<List<Cart>> GetAllCart( string userid);
        Task<List<Cart>> GetCart(string cartId, string userid);
        Task<Cart> UpdateCart(string BookTitle, string Author, int quantity, string userid);
    }
}
