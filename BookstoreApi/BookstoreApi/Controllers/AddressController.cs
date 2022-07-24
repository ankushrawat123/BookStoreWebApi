using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;
        private readonly IConfiguration configuration;
        private readonly IMongoCollection<Address> addresses;
        public AddressController(IAddressBL addressBL, IConfig _config, IConfiguration configuration)
        {
            this.addressBL = addressBL;
            this.configuration = configuration;
            var addressClient = new MongoClient(_config.ConnectionString);
            var database = addressClient.GetDatabase(_config.DatabaseName);
            addresses = database.GetCollection<Address>("addresses");

        }

        [Authorize]
        [HttpPost]
        [Route("AddAddress")]
        public async Task<IActionResult> AddAddress(AddressModel addressModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = userid.Value;
                if(UserID!=null)
                {
                    var addressData = await addressBL.AddAddress(UserID, addressModel);
                    return Ok(new { status = true, Message = "Address Added Successfully",data=addressData });
                }
                return BadRequest(new { status = false, Message = "Address Not Added " });
            }
            catch(Exception e)
            {
                return NotFound(new {status=false, message=e.Message});
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAllAddress")]
        public async Task<IActionResult> GetAllAddress()
        {
            try
            {
                var userid= User.Claims.FirstOrDefault(x=>x.Type.ToString().Equals("UserId",StringComparison.InvariantCultureIgnoreCase));
                var UserID = userid.Value;
                if(UserID!=null)
                {
                    List<Address> AddressList = new List<Address>();
                    AddressList = await addressBL.GetAllAddress(UserID);
                    return Ok(new { status = true, Message = "All Address Obtained Successfully!",data=AddressList});
                }
                return BadRequest(new { status = false, Message = "Address Not Obtained " });      
            }
            catch (Exception e)
            {
                return NotFound(new {status=false,message=e.Message});
            }
        }

        [Authorize]
        [HttpGet]
        [Route("GetAddress/{addressId}")]

        public async Task<IActionResult> GetAddress(string addressId)
        {
            try
            {
                var userid= User.Claims.FirstOrDefault(x=>x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = userid.Value;

                var addresscheck =  addresses.AsQueryable().Where(x => x.UserId == UserID && x.AddressId == addressId);

                if (addresscheck != null)
                {
                    var addressData = await addressBL.GetAddress(UserID, addressId);
                    return Ok(new { status = true, Message = "Got One Address Successfully", data = addressData });
                }
                return BadRequest(new { status = false, Message = "Address Not Obtained" });
            }
            catch(Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteAddress/{addressId}")]
        public async Task<IActionResult> DeleteAddress(string addressId)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = userid.Value;
                var addressCheck = addresses.AsQueryable().Where(x=>x.UserId==UserID && x.AddressId == addressId);
               if(addressCheck != null)
                {
                    await addressBL.DeleteAddress(UserID, addressId);
                    return Ok(new { status = true, Message = "Address Deleted Successfully" });
                }
                return BadRequest(new { status=false,Message="Address Not Deleted" });
            
            }
            catch (Exception e)
            {
                return NotFound(new {status=false,Message=e.Message});
            }
        }

        [Authorize]
        [HttpPut]
        [Route("UpdateAddress/{addressId}")]

        public async Task<IActionResult> UpdateAddress(string addressId,AddressModel addressModel)
        {
            try
            {
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("UserId", StringComparison.InvariantCultureIgnoreCase));
                var UserID = userid.Value;
                var addressData = addresses.AsQueryable().Where(x => x.UserId == UserID && x.AddressId == addressId);
                if (addressData != null)
                {
                   var data1= await addressBL.UpdateAddress(UserID, addressId, addressModel);
                    return Ok(new { status = true, Message = "Address Updated Successfully" ,data=data1});
                }
                return BadRequest(new { status = false, Message = "Address Not Updated" });
            }
            catch (Exception e)
            {
                return NotFound(new { status = false, Message = e.Message });
            }
        }
    }
}
