using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BiBiBE.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiBiBE.DAO
{
    public class OrderDAO
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }
        public static async Task<List<Order>> GetOrders()
        {
            var services = new List<Order>();

            try
            {
                using (var context = new BiBiContext())
                {
                    services = await context.Orders.ToListAsync();

                }
                return services;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task AddOrder(Order m)
        {
            try
            {


                using (var context = new BiBiContext())
                {
                    var p2 = await context.Orders.FirstOrDefaultAsync(c => c.OrderId.Equals(m.OrderId));

                    if (p2 == null)
                    {
                        context.Orders.Add(m);
                        await context.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("Id is Exits");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateOrder(Order m)
        {
            try
            {
                using (var context = new BiBiContext())
                {


                    context.Entry<Order>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteOrder(int p)
        {
            try
            {
                using (var context = new BiBiContext())
                {
                    var service = await context.Orders.FirstOrDefaultAsync(c => c.OrderId == p);
                    if (service == null)
                    {
                        throw new Exception("Id is not Exits");
                    }
                    else
                    {
                        context.Orders.Remove(service);
                        await context.SaveChangesAsync();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<Order> GetOrderById(int TypeId)
        {

            IEnumerable<Order> services = await GetOrders();
            Order service = services.SingleOrDefault(mb => mb.OrderId == TypeId);
            return service;
        }
    }
}
