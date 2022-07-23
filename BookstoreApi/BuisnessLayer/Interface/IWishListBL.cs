using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface IWishListBL
    {
        Task<WishList> AddWishList(string bookid, string userid);

        Task<List<WishList>> GetAllWishList(string userid);
        Task<List<WishList>> GetWishList(string userid, string wishlistid);
        Task DeleteWishList(string Userid,string wishlistid);
    }
}
