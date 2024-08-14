using Microsoft.AspNetCore.Mvc;
using ProductInventoryApp.DTO;
using ProductInventoryApp.Models;
using ProductInventoryApp.Services.Interfaces;
using ProductInventoryApp.Services.Providers;
using System.Net.Mime;

namespace ProductInventoryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Suppliers))]
    public class SuppliersController : ControllerBase
    {
        private readonly ISuppliersServices _suppliersService;
        private readonly ILogger<SuppliersController> _logger;
        public SuppliersController(ISuppliersServices suppliersService, ILogger<SuppliersController>logger)
        {
            _logger = logger;
            _suppliersService = suppliersService;
        }



        [HttpGet("allSuppliers")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<SuppliersDto>))]
        public async Task<IActionResult> GetSuppliers()
        {
            var suppliers = await _suppliersService.GetSuppliers();
            return Ok(suppliers);
        }

        [HttpPost("createSupplier")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(201, Type = typeof(SuppliersDto))]
        public async Task<IActionResult> CreateProduct([FromBody] SuppliersDto suppliersDto)
        {
            var supplier = await _suppliersService.CreateSupplier(suppliersDto);
            return CreatedAtAction("GetSupplier", new { id = supplier.Id }, suppliersDto);
        }

        [HttpPut("updatesSupplier/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateSupplier(string id, [FromBody] SuppliersDto suppliersDto)
        {
            var supplier = await _suppliersService.UpdateSupplier(id, suppliersDto);
            return Ok(supplier);
        }

        [HttpGet("getSupplier/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(200, Type = typeof(SuppliersDto))]
        public async Task<IActionResult> GetSupplier(string id)
        {
            var supplier = await _suppliersService.GetSupplier(id);
            return Ok(supplier);
        }

        [HttpDelete("deleteSupplier/{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteSupplier(string id)
        {
            var supplier = await _suppliersService.DeleteSupplier(id);
            return Ok(supplier);
        }
    }
}
