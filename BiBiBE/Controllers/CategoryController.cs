using BiBiBE.Models;
using BiBiBE.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace BiBiBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IConfiguration configuration;
        public CategoryController(ICategoryRepository _categoryRepository, IConfiguration configuration)
        {
            categoryRepository = _categoryRepository;
            this.configuration = configuration;

        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int page, int pageSize)
        {
            try
            {
                var TypeList = await categoryRepository.SearchByName(search, page, pageSize);
                var Count = TypeList.Count();
                return Ok(new { StatusCode = 200, Message = "Load successful", data = TypeList, Count });

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            try
            {
                var Result = await categoryRepository.GetCategoryById(id);
                return Ok(new { StatusCode = 200, Message = "Load successful", data = Result });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }
        }

        [HttpPost]

        public async Task<IActionResult> Create(Category category)
        {

            try
            {
                Category newCate = new Category
                {
                    CategoryId = category.CategoryId,
                    Title = category.Title,
                    Description = category.Description
                };
                await categoryRepository.AddCategory(newCate);
                return Ok(new { StatusCode = 200, Message = "Add successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }

        [HttpPut("{id}")]

        public async Task<IActionResult> update(int id, Category category)
        {
            if (id != category.CategoryId)
            {
                return BadRequest();
            }
            try
            {
                Category newCate = new Category
                {

                    CategoryId = category.CategoryId,
                    Title = category.Title,
                };
                await categoryRepository.UpdateCategory(newCate);
                return Ok(new { StatusCode = 200, Message = "Update successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }
        }



        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                await categoryRepository.DeleteCategory(id);

                return Ok(new { StatusCode = 200, Message = "Delete successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }
    }
}
