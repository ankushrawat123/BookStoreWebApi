using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Interface
{
    public interface IAddressBL
    {
        Task<Address> AddAddress(string userid, AddressModel addressModel);
        Task<List<Address>> GetAllAddress(string userid);
        Task<List<Address>> GetAddress(string userid,string addressId);
        Task DeleteAddress(string userid,string addressId);
        Task<Address> UpdateAddress(string userid,string addressId,AddressModel addressModel);
    }
}
