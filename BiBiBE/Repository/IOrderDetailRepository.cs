using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public interface IOrderDetailRepository
    {
        Task<List<OrderDetail>> GetOrderDetails();
        Task AddOrderDetail(OrderDetail m);
        Task UpdateOrderDetail(OrderDetail m);
    }
}
