using BiBiBE.DAO;
using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public Task<List<OrderDetail>> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();
        public Task AddOrderDetail(OrderDetail m) => OrderDetailDAO.AddOrderDetail(m);
        public Task UpdateOrderDetail(OrderDetail m) => OrderDetailDAO.UpdateOrderDetail(m);
    }
}
