using BuisnessLayer.Interface;
using DatabaseLayer.Cart;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Service
{
    public class CartBL:ICartBL
    {
        private readonly ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
                this.cartRL = cartRL;
        }

        public async Task<Cart> AddCart(CartPostModel cartPostModel,string userid)
        {
            try
            {
               return await cartRL.AddCart(cartPostModel,userid);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteCart(string cartId,string userid)
        {
            try
            {
                await cartRL.DeleteCart(cartId,userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Cart>> GetAllCart( string userid)
        {
            try
            {
                return await cartRL.GetAllCart(userid);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public async Task<List<Cart>> GetCart(string cartId, string userid)
        {
            try
            {
                return await cartRL.GetCart(cartId, userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<Cart> UpdateCart(string BookTitle, string Author, int quantity, string userid)
        {
            try
            {
                 return await cartRL.UpdateCart(BookTitle,Author,quantity,userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
