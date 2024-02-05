using CodeBuddies_PizzaAPI.DTOs;
using CodeBuddies_PizzaAPI.Models;

namespace CodeBuddies_PizzaAPI.Services
{
    public interface IProductService
    {
        Task<ProductResponseDTO> AddProductAsync(ProductRequest product);
        Task<IEnumerable<ProductResponseDTO>> GetAllProductsAsync();
        Task<ProductResponseDTO> GetProductByIdAsync(int id);
        Task<ProductResponseDTO> UpdateProductAsync(int id, ProductRequest product);
        Task<bool> DeleteProductAsync(int id);

    }
}
