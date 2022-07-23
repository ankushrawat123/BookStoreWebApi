using BuisnessLayer.Interface;
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
    [Route("Api/[controller]")]
    public class WishListController : ControllerBase
    {
        private readonly IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }

        [Authorize]
        [HttpPost]
        [Route("AddWishList/{bookid}")]
        public async Task<IActionResult> AddWishList(string bookid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string UserId = userid.Value;
                if (userid != null)
                {
                    var wishData = await wishListBL.AddWishList(bookid, UserId);
                    return Ok(new { Status = true, Message = "WishList Is Added Successfully" });
                }
                return BadRequest(new { Status = true, Message = "User Doesn't exist" });
            }
            catch (Exception e)
            {
                return NotFound(new { Status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllWishList")]

        public async Task<IActionResult> GetAllWishList()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string UserId = userid.Value;
                if (UserId != null)
                {

                    List<WishList> wish = new List<WishList>();
                    wish = await wishListBL.GetAllWishList(UserId);
                    return Ok(new { status = true, Message = "Got All WishList Successfully", data = wish });

                }
                return BadRequest(new { status = false, Message = "UserId doesn't found" });
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetWishList /{wishlistid}")]

        public async Task<IActionResult> GetWishlist(string wishlistid)
        {
            try
            {
            var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
            string UserID = userid.Value;
            if(UserID!= null)
            {
                List<WishList> wish = new List<WishList>();
                wish = await wishListBL.GetWishList(UserID, wishlistid);
                return Ok(new { status = true, Message = "Got One Wishlist Successfully",data=wish });
            }
            return BadRequest(new { status = false, Message = "UserId doesn't found" });
            }
            catch(Exception e)
            {
                return NotFound(new {Status=false, Message=e.Message});
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteWishList/{wishListId}")]

        public async Task<IActionResult> DeleteWishList(string wishListId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string UserID = userid.Value;
                if (UserID != null)
                {
                    await wishListBL.DeleteWishList(UserID, wishListId);
                    return Ok(new { status = true, Message = "WishList Deleted Successfully" });
                }
                return BadRequest(new { status = false, Message = "User doesn't Found" });
            }
            catch (Exception e)
            {
                return NotFound(new { success = false, Message = e.Message });
            }
        }
    }
}
