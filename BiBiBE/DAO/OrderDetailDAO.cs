using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BiBiBE.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace BiBiBE.DAO
{
    public class OrderDetailDAO
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }
        public static async Task<List<OrderDetail>> GetOrderDetails()
        {
            var serviceInBills = new List<OrderDetail>();

            try
            {
                using (var context = new BiBiContext())
                {
                    serviceInBills = await context.OrderDetails.ToListAsync();

                }
                return serviceInBills;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task AddOrderDetail(OrderDetail m)
        {
            try
            {
                using (var context = new BiBiContext())
                {

                    context.OrderDetails.Add(m);
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateOrderDetail(OrderDetail m)
        {
            try
            {
                using (var context = new BiBiContext())
                {


                    context.Entry<OrderDetail>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
