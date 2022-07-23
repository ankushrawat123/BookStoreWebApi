using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        Task<WishList> AddWishList(string bookid, string userid);
        Task<List<WishList>> GetAllWishList(string bookid);
        Task<List<WishList>> GetWishList(string userid,string wishlistid);
        Task DeleteWishList(string Userid,string wishlistid);
    }
}
