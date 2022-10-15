using BiBiBE.DAO;
using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task<List<Product>> GetProducts() => ProductDAO.GetProducts();

        public Task<Product> GetProductById(int Id) => ProductDAO.Instance.GetProductById(Id);
        public Task DeleteProduct(int m) => ProductDAO.DeleteProduct(m);


        public Task AddProduct(Product m) => ProductDAO.AddProduct(m);
        public Task UpdateProduct(Product m) => ProductDAO.UpdateProduct(m);
        public Task<List<Product>> SearchByName(string search, int page, int pageSize) => ProductDAO.Instance.SearchByTitle(search, page, pageSize);
    }
}
