using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        Task<Order> AddOrder(string userid, OrderPostModel orderPostModel);

        Task<List<Order>> GetAllOrders(string userid);

        Task DeleteOrder(string orderId, string userid);

        Task<List<Order>> GetOrder(string orderId, string userid);
    }
}
