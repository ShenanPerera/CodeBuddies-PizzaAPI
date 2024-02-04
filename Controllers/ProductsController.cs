using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeBuddies_PizzaAPI.Models;
using CodeBuddies_PizzaAPI.Services;

namespace CodeBuddies_PizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        readonly private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                var newProduct = await _productService.AddProductAsync(product);
                return CreatedAtAction("GetProduct", new { id = newProduct.Id }, newProduct);
            }
            catch (DbUpdateConcurrencyException e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            IEnumerable<Product> products = await _productService.GetAllProductsAsync();
            if (!products.Any())
            {
                return NotFound("No products found in the database");
            }
            return Ok(products);
        }

        // GET: api/Products/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetPRoduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            if (updatedProduct == null)
            {
                return NotFound($"Product with id: {id} does not exist in the database");
            }
            return Ok(updatedProduct);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound("Product not found");
            }
            return Ok("Product is removed successfully");
        }
    }
}

