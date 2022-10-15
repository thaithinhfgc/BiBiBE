using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProducts();

        Task<Product> GetProductById(int Id);

        Task DeleteProduct(int m);

        Task UpdateProduct(Product m);
        Task AddProduct(Product m);

        Task<List<Product>> SearchByName(string search, int page, int pageSize);
    }
}
