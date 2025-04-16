using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArchitectureChallenge.Application.Interfaces;
using CleanArchitectureChallenge.Application.DTOs;
using Swashbuckle.AspNetCore.Annotations;

namespace CleanArchitectureChallenge.API.Controllers
{
    /// <summary>
    /// API controller for product operations.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductsController(IProductService service) => _service = service;

        /// <summary>
        /// Get all products.
        /// </summary>
        [HttpGet]
        [SwaggerOperation(Summary = "Retrieve all products.")]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllProductsAsync());

        /// <summary>
        /// Get a product by ID.
        /// </summary>
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Retrieve product by ID.")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _service.GetProductByIdAsync(id);
            return product == null ? NotFound() : Ok(product);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        [HttpPost]
        [SwaggerOperation(Summary = "Create a new product.")]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            await _service.CreateProductAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        /// <summary>
        /// Update an existing product.
        /// </summary>
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an existing product.")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProductDto dto)
        {
            if (id != dto.Id)
                return BadRequest();
            await _service.UpdateProductAsync(dto);
            return NoContent();
        }

        /// <summary>
        /// Delete a product by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete a product.")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
