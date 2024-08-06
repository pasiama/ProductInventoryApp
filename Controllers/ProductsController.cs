using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductInventoryApp.DatabaseContext;
using ProductInventoryApp.DTO;
using ProductInventoryApp.Interfaces;
using ProductInventoryApp.Models;
using ProductInventoryApp.Services.Interfaces;
using ProductInventoryApp.Services.Providers;
using System.Linq.Expressions;
using System.Net.Mime;

namespace ProductInventoryApp.Controllers
{
   
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Product))]
    public class ProductsController : ControllerBase
    {
       
        private readonly IProductServices _productServices;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductServices productservice, ILogger<ProductsController> logger)
        {
            _productServices = productservice;
            _logger = logger;
           
        }




        /// <summary>
        ///  Get all details 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("allProducts")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]

        public async Task<IActionResult> GetProducts()
        {
            var products = await _productServices.GetProducts();
            return Ok(products);
        }

        [HttpPost("createProduct")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(201, Type = typeof(ProductRequestDto))]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequestDto productDto)
        {
                var product = await _productServices.CreateProduct(productDto);
                return CreatedAtAction("GetProduct", new { id = product.Id }, productDto);
        }

        [HttpPut("updatesProduct/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateProduct(string id, [FromBody] ProductRequestDto productDto)
        {
                var product = await _productServices.UpdateProduct(id, productDto);
                return Ok(product);
        }

        [HttpGet("getProduct/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(200, Type = typeof(ProductRequestDto))]
        public async Task<IActionResult> GetProduct(string id)
        {
           var products = await _productServices.GetProduct(id);
           return Ok(products);
        }

        [HttpDelete("deleteProduct/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProduct(string id)
        {
           var products = await _productServices.DeleteProduct(id);
           return Ok(products);
        }
    } 
}
