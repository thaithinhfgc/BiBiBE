using BiBiBE.DAO;
using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public Task<List<Order>> GetOrders() => OrderDAO.GetOrders();
        public Task<Order> GetOrderById(int ServiceId) => OrderDAO.Instance.GetOrderById(ServiceId);
        public Task DeleteOrder(int m) => OrderDAO.DeleteOrder(m);
        public Task AddOrder(Order m) => OrderDAO.AddOrder(m);
        public Task UpdateOrder(Order m) => OrderDAO.UpdateOrder(m);
    }
}
