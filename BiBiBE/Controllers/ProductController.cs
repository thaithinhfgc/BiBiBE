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
    public class ProductController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IConfiguration configuration;
        public ProductController(IProductRepository _productRepository, IConfiguration configuration)
        {
            productRepository = _productRepository;
            this.configuration = configuration;

        }


        [HttpGet]
        public async Task<IActionResult> GetAll(string search, int page, int pageSize)
        {

            try
            {
                var TypeList = await productRepository.SearchByName(search, page, pageSize);
                var Count = TypeList.Count();
                return Ok(new { StatusCode = 200, Message = "Load successful", data = TypeList, Count });

            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            try
            {
                var Result = await productRepository.GetProductById(id);
                return Ok(new { StatusCode = 200, Message = "Load successful", data = Result });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }
        }

        [HttpPost]

        public async Task<IActionResult> Create(Product product)
        {

            try
            {
                Product newFilm = new Product
                {

                    Title = product.Title,
                    Brand = product.Brand,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,

                };
                await productRepository.AddProduct(newFilm);
                return Ok(new { StatusCode = 200, Message = "Add successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }

        [HttpPut("{id}")]

        public async Task<IActionResult> update(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }
            try
            {
                Product newFilm = new Product
                {

                    ProductId = product.ProductId,
                    Title = product.Title,
                    Brand = product.Brand,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,

                };
                await productRepository.UpdateProduct(newFilm);
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

                await productRepository.DeleteProduct(id);

                return Ok(new { StatusCode = 200, Message = "Delete successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(409, new { StatusCode = 409, Message = ex.Message });
            }


        }
    }
}
