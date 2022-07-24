using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Service
{
    
    public class AddressRL:IAddressRL
    {
        private readonly IMongoCollection<Register> _user;
        private readonly IMongoCollection<Address> addresses;
        private readonly IConfiguration configuration;

        public AddressRL(IConfiguration configuration,IConfig _config)
        {
            this.configuration = configuration;
            var addressClient = new MongoClient(_config.ConnectionString);
            var database = addressClient.GetDatabase(_config.DatabaseName);
            addresses = database.GetCollection<Address>("addresses");
            _user = database.GetCollection<Register>("_user");
        }

        public async Task<Address> AddAddress(string userid, AddressModel addressModel)
        {
            try
            {
                var addressData = await addresses.AsQueryable().Where(x => x.UserId == userid).SingleOrDefaultAsync();
                var userData = await _user.AsQueryable().Where(x => x.UserId == userid).SingleOrDefaultAsync();
                Address addressObj = new Address();
                Register registerObj = new Register();

                registerObj.UserId = userid;
                registerObj.FullName = userData.FullName;
                registerObj.EmailId = userData.EmailId;
                registerObj.Password = userData.Password;
                registerObj.ContactNumber=userData.ContactNumber;

                addressObj.UserId = userid;
                addressObj.addressTypeId = addressModel.addressTypeId;
                addressObj.Addresses = addressModel.Addresses;
                addressObj.City=addressModel.City;
                addressObj.State=addressModel.State;
                addressObj.Pincode = addressModel.Pincode;

                addressObj.register=registerObj;
                await addresses.InsertOneAsync(addressObj);
                return addressObj;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteAddress(string userid, string addressId)
        {
            try
            {
                var addressCheck = addresses.AsQueryable().Where(x => x.UserId == userid && x.AddressId == addressId).SingleOrDefaultAsync();
           
                if(addressCheck!=null)
                {
                   await addresses.DeleteOneAsync(x=>x.AddressId==addressId);
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Address>> GetAddress(string userid, string addressId)
        {
            try
            {
                var addressData= await addresses.AsQueryable().Where(x=>x.UserId==userid&&x.AddressId==addressId).SingleOrDefaultAsync();
                if (addressData != null)
                {
                    return await addresses.Find(x => x.AddressId == addressId).ToListAsync();
                }
                else 
                    return null;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Address>> GetAllAddress(string userid)
        {
            try
            {
                var addressData= addresses.AsQueryable().Where(x => x.UserId == userid);
                if(addressData==null)
                {
                    return null;
                }
                return await addresses.Find(_ => true).ToListAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<Address> UpdateAddress(string userid, string addressId, AddressModel addressModel)
        {
            try
            {
                var addressData =  addresses.AsQueryable().Where(x => x.UserId == userid);
                if (addressData == null)
                {
                    return null;
                }
                await addresses.UpdateOneAsync(x => x.AddressId ==addressId,
                       Builders<Address>.Update.Set(x => x.addressTypeId, addressModel.addressTypeId)
                       .Set(x => x.addressTypeId, addressModel.addressTypeId)
                       .Set(x => x.Addresses, addressModel.Addresses)
                       .Set(x => x.City, addressModel.City)
                       .Set(x => x.State, addressModel.State)
                       .Set(x => x.Pincode, addressModel.Pincode)
                       );
                return await addresses.AsQueryable().Where(x => x.AddressId == addressId).SingleOrDefaultAsync();
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
