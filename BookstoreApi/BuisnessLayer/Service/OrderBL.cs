using BuisnessLayer.Interface;
using RepositoryLayer.Interface;
using RepositoryLayer.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessLayer.Service
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;

        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public async Task<Order> AddOrder(string userid, OrderPostModel orderPostModel)
        {
            try
            {
                return await orderRL.AddOrder(userid, orderPostModel);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Order>> GetAllOrders(string userid)
        {
            try
            {
                return await orderRL.GetAllOrders(userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteOrder(string orderId, string userid)
        {
            try
            {
                
               await orderRL.DeleteOrder(orderId,userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<List<Order>> GetOrder(string orderId, string userid)
        {
            try
            {
                return await orderRL.GetOrder(orderId, userid);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
