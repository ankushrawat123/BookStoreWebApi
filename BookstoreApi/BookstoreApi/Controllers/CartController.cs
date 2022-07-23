using BuisnessLayer.Interface;
using DatabaseLayer.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize]
        [HttpPost]
        [Route("AddCart")]
        public async Task<IActionResult> AddCart(CartPostModel cartPostModel)
        {
            //var cart = 
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string UserID = userid.Value;
                if (userid != null)
                {
                    var cartData = await cartBL.AddCart(cartPostModel, UserID);
                    return Ok(new { Status = true, Message = "Cart Added Successfully", data = cartData });
                }
                else
                    return BadRequest(new { Status = false, Message = "User Doesn't exist, Please registered the User" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteCart /{cartid}")]
        public async Task<IActionResult> DeleteCart(string cartid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string userId = userid.Value;
                if (userid != null)
                {
                    await cartBL.DeleteCart(cartid, userId);
                    return Ok(new { Status = true, Message = "Cart Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "No Cart found with this Id" });
                }
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetCart/{cartid}")]

        public async Task<IActionResult> GetCart(string cartid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string userId = userid.Value;
                if (userId != null)
                {
                    List<Cart> cartList = new List<Cart>();
                    cartList = await cartBL.GetCart(cartid, userId);
                    return Ok(new { Status = true, Message = "Got One Cart Successfully", data = cartList });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "CartId Not Exist" });
                }

            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllCart")]
        public async Task<IActionResult> GetAllCart()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string userId = userid.Value;
                if (userId != null)
                {
                    List<Cart> cartList = new List<Cart>();
                    cartList = await cartBL.GetAllCart(userId);
                    return Ok(new { Status = true, Message = "Got All Cart Successfully", data = cartList });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "CartId Not Exist" });
                }
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateCart/{BookTitle}/{Author}/{quantity}")]
        public async Task<IActionResult> UpdateCart(string BookTitle, string Author, int quantity)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string userId = userid.Value;
                
                if(userId!=null)
                {
                    var cartdata = await cartBL.UpdateCart(BookTitle,Author,quantity,userId);
                    return Ok(new {status=true,Message="Cart Updated Successfully",data=cartdata});
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "Cart not Found " });
                }
            }
            catch(Exception e)
            {
                return NotFound(new { status=false, Message = e.Message});
            }
        }
    }
}
