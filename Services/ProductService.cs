using CodeBuddies_PizzaAPI.Data;
using CodeBuddies_PizzaAPI.DTOs;
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

        public async Task<ProductResponseDTO> AddProductAsync(ProductRequest productDTO)
        {     

            var product = new Product
            {
                Name = productDTO.Name,
                Price = productDTO.Price
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var ProductResponseDTO = new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return ProductResponseDTO;
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllProductsAsync()
        {

            var products = await _context.Products.ToListAsync();
            var productsDTO = new List<ProductResponseDTO>();
            foreach (var product in products)
            {

                  productsDTO.Add(new ProductResponseDTO
                  {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price
                });


            }
           return productsDTO;
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return null;
            }

            var productDTO = new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            return productDTO;
            
        }

        public async Task<ProductResponseDTO> UpdateProductAsync(int id, ProductRequest productDTO)
        {

            var existingProduct = await _context.Products.FindAsync(id);

            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with id: {id} does not exist in the database.");
            }

            existingProduct.Price = productDTO.Price;
            existingProduct.Name = productDTO.Name;

           
                await _context.SaveChangesAsync();




            var productResponseDTO = new ProductResponseDTO
            {
                Id = existingProduct.Id,
                Name = existingProduct.Name,
                Price = existingProduct.Price
            };
            return productResponseDTO;
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
