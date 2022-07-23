using BuisnessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Service
{
  
    public class WishListBL:IWishListBL
    {
        private readonly IWishListRL wishListRL;

        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public async Task<WishList> AddWishList(string bookid, string userid)
        {
            try
            {
                return await wishListRL.AddWishList(bookid, userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteWishList(string Userid, string wishlistid)
        {
            try
            {
                 await wishListRL.DeleteWishList(Userid, wishlistid);
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
                return await wishListRL.GetAllWishList(userid);
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
                return await wishListRL.GetWishList(userid,wishlistid);            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
