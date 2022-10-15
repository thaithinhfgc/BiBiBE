using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiBiBE.DAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public static async Task<List<Product>> GetProducts()
        {
            var products = new List<Product>();

            try
            {
                using (var context = new BiBiContext())
                {
                    products = await context.Products.ToListAsync();

                }
                return products;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public static async Task AddProduct(Product m)
        {
            try
            {


                using (var context = new BiBiContext())
                {
                    var p1 = await context.Products.FirstOrDefaultAsync(c => c.Title.Equals(m.Title));
                    var p2 = await context.Products.FirstOrDefaultAsync(c => c.ProductId.Equals(m.ProductId));
                    if (p1 == null)
                    {
                        if (p2 == null)
                        {
                            context.Products.Add(m);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            throw new Exception("Id is Exits");
                        }
                    }
                    else
                    {
                        throw new Exception("Produc tName is Exist");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task UpdateProduct(Product m)
        {
            try
            {
                using (var context = new BiBiContext())
                {


                    context.Entry<Product>(m).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static async Task DeleteProduct(int p)
        {
            try
            {
                using (var context = new BiBiContext())
                {
                    var film = await context.Products.FirstOrDefaultAsync(c => c.ProductId == p);
                    if (film == null)
                    {
                        throw new Exception("Id is not Exits");
                    }
                    else
                    {
                        context.Products.Remove(film);
                        await context.SaveChangesAsync();
                    }



                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<List<Product>> SearchByTitle(string search, int page, int pageSize)
        {
            List<Product> searchResult = null;
            if (page == 0 || pageSize == 0)
            {
                page = 1;
                pageSize = 1000;
            }
            if (search == null)
            {
                IEnumerable<Product> searchValues = await GetProducts();
                searchValues = searchValues.Skip((page - 1) * pageSize).Take(pageSize);
                searchResult = searchValues.ToList();
            }
            else
            {
                using (var context = new BiBiContext())
                {
                    IEnumerable<Product> searchValues = await (from film in context.Products
                                                               where film.Title.ToLower().Contains(search.ToLower())
                                                               select film).ToListAsync();
                    searchValues = searchValues.Skip((page - 1) * pageSize).Take(pageSize);
                    searchResult = searchValues.ToList();
                }
            }

            return searchResult;
        }
        public async Task<Product> GetProductById(int Id)
        {

            IEnumerable<Product> products = await GetProducts();
            Product product = products.SingleOrDefault(mb => mb.ProductId == Id);
            return product;
        }

    }
}
