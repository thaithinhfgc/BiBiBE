using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrders();
        Task<Order> GetOrderById(int ServiceId);
        Task DeleteOrder(int m);
        Task UpdateOrder(Order m);
        Task AddOrder(Order m);
    }
}
