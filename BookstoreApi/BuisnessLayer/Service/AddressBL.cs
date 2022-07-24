using BuisnessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Service
{
    public class AddressBL : IAddressBL
    {
        private readonly IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public async Task<Address> AddAddress(string userid, AddressModel addressModel)
        {
            try
            {
            return await this.addressRL.AddAddress(userid, addressModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteAddress(string userid, string addressId)
        {
            try
            {
                await addressRL.DeleteAddress(userid, addressId);
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
                return await addressRL.GetAddress(userid,addressId);
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
                return await addressRL.GetAllAddress(userid);
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
                return await addressRL.UpdateAddress(userid,addressId,addressModel);
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
