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

        //RETURNS A LIST OF PRODUCTS
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]
        public async Task<IActionResult> GetProducts()
        { 
        
            var products =  _productRepository.GetProducts();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

               
            }
            return Ok(products);
        }

        //CREATES A PRODUCT
        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Product))]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productRepository.Add(product);
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        //UPDATES A PRODUCT
        [HttpPut("{productId}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            if (!_productRepository.ProductExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productRepository.Update(product);
            return NoContent();
        }



        //RETURNS A PRODUCT BY ID
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        public async Task<IActionResult> GetProduct(int Productid)
        {
            var product = _productRepository.GetById(Productid);
            if(!_productRepository.ProductExists(Productid))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(product);
        }


        //DELETES A PRODUCT
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (!_productRepository.ProductExists(id))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _productRepository.Delete(id);
            return NoContent();
        }


        //RETURNS A PRODUCT BY NAME
        /* [HttpGet("{productname}")]
         [ProducesResponseType(200, Type = typeof(Product))]
         public IActionResult GetProduct(string Productname)
         {
             var product = _productRepository.GetByName(Productname);
             if (product == null)
             {
                 return NotFound();
             }

             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             return Ok(product);
         }*/


        //RETURNS A PRODUCT BY DESCRIPTION
        /* [HttpGet("{description}")]
         [ProducesResponseType(200, Type = typeof(Product))]
         public IActionResult GetProductByDescription(string Productdescription)
         {
             var product = _productRepository.GetByDescription(Productdescription);
             if (product == null)
             {
                 return NotFound();
             }

             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             return Ok(product);
         }*/


        //RETURNS A PRODUCT BY PRICE
        /* [HttpGet("{price}")]
         [ProducesResponseType(200, Type = typeof(Product))]
         public IActionResult GetProductByPrice(decimal Productprice)
         {
             var product = _productRepository.GetByPrice(Productprice);
             if (product == null)
             {
                 return NotFound();
             }

             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             return Ok(product);
         }*/


        //RETURNS A PRODUCT BY QUANTITY
        /*[HttpGet("{quantity}")]
            [ProducesResponseType(200, Type = typeof(Product))]
            public IActionResult GetProductByQuantity(int Productquantity)
            {
                var product = _productRepository.GetByQuantity(Productquantity);
                if (product == null)
                {
                    return NotFound();
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(product);
            }*/

        //CREATES A PRODUCT
        /*

        



         //DELETES A PRODUCT BY NAME
         [HttpDelete("{name}")]
         [ProducesResponseType(204)]
         public IActionResult DeleteProduct(string name)
         {
             var product = _productRepository.GetByName(name);
             if (product == null)
             {
                 return NotFound();
             }

             if (!ModelState.IsValid)
             {
                 return BadRequest(ModelState);
             }

             _productRepository.Delete(product.Id);
             return NoContent();
         }*/



    }
}
