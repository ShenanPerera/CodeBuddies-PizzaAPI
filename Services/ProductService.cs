using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeBuddies_PizzaAPI.Services
{
    public class ProductService : IProductService
    {

        private readonly PizzaContext _context;

        public ProductService(PizzaContext context)
        {
            _context = context;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> UpdateProductAsync(int id, Product product)
        {
            if (id != product.Id)
            {
                throw new ArgumentException("The provided ID does not match the product ID in the request body.");
            }

            var existingProdut = await _context.Products.FindAsync(id);

            if (existingProdut == null)
            {
                throw new KeyNotFoundException($"Product with id: {id} does not exist in the database.");
            }

            existingProdut.Price = product.Price;
            existingProdut.Name = product.Name;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Products.AnyAsync(a => a.Id == id))
                {
                    throw new KeyNotFoundException($"Product with id: {id} does not exist in the database.");
                }
                else
                {
                    throw;
                }
            }

            return existingProdut;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
