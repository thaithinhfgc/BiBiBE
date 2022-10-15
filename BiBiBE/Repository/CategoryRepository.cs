using BiBiBE.DAO;
using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task<List<Category>> GetCategorys() => CategoryDAO.GetCategorys();

        public Task<Category> GetCategoryById(int Id) => CategoryDAO.Instance.GetProductById(Id);
        public Task DeleteCategory(int m) => CategoryDAO.DeleteCategory(m);


        public Task AddCategory(Category m) => CategoryDAO.AddCategory(m);
        public Task UpdateCategory(Category m) => CategoryDAO.UpdateCategory(m);
        public Task<List<Category>> SearchByName(string search, int page, int pageSize) => CategoryDAO.Instance.SearchByTitle(search, page, pageSize);
    }
}
