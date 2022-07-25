using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Order> orders;
        public OrderController(IOrderBL orderBL, IConfiguration configuration, IConfig _config)
        {
            this.orderBL = orderBL;
            this.configuration = configuration;
            var addressClient = new MongoClient(_config.ConnectionString);
            var database = addressClient.GetDatabase(_config.DatabaseName);
            orders = database.GetCollection<Order>("orders");
        }

        [Authorize]
        [HttpPost]
        [Route("AddOrder")]

        public async Task<IActionResult> AddOrder(OrderPostModel orderPostModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID=userid.Value;

               
                if(UserID!=null)
                {
                    var orderData = orderBL.AddOrder(UserID, orderPostModel);
                    return Ok(new {success=true,Message="Order Placed Successfully",data=orderData});
                }
                return BadRequest(new { status = false, Message = "Order Not Placed" });
            
            }
            catch(Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = userid.Value;
                if (UserID != null)
                {
                    List<Order> OrderList = new List<Order>();
                    OrderList = await orderBL.GetAllOrders(UserID);
                    return Ok(new { status = true, Message = "All Orders Obtained Successfully!", data = OrderList });
                }
                return BadRequest(new { status = false, Message = "Orders Not Obtained " });
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, message = e.Message });
            }
        }


        [Authorize]
        [HttpDelete]
        [Route("DeleteOrder /{orderid}")]
        public async Task<IActionResult> DeleteCart(string orderid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string userId = userid.Value;
                if (userid != null)
                {
                    await orderBL.DeleteOrder(orderid, userId);
                    return Ok(new { Status = true, Message = "Order Deleted Successfully" });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "No Order found with this Id" });
                }
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }


        [Authorize]
        [HttpGet]
        [Route("GetOrder/{orderid}")]

        public async Task<IActionResult> GetCart(string orderid)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                string userId = userid.Value;
                if (userId != null)
                {
                    List<Order> orderList = new List<Order>();
                    orderList = await orderBL.GetOrder(orderid, userId);
                    return Ok(new { Status = true, Message = "Got One Cart Successfully", data = orderList });
                }
                else
                {
                    return BadRequest(new { Status = false, Message = "OrderId Not Exist" });
                }

            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }


    }
}
