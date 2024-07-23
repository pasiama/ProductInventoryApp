using Microsoft.AspNetCore.Mvc;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;

namespace ProductInventoryApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository repository)
        {
            _productRepository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public IActionResult GetProducts()
        { 
        
            var products = _productRepository.GetProducts();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

               
            }
            return Ok(products);
        }
      }
}
