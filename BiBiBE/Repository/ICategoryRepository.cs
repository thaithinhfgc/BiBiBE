using BiBiBE.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiBiBE.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategorys();

        Task<Category> GetCategoryById(int Id);

        Task DeleteCategory(int m);

        Task UpdateCategory(Category m);
        Task AddCategory(Category m);

        Task<List<Category>> SearchByName(string search, int page, int pageSize);
    }
}
