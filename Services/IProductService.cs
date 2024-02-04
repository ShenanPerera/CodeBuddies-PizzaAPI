using CodeBuddies_PizzaAPI.Models;

namespace CodeBuddies_PizzaAPI.Services
{
    public interface IProductService
    {
        Task<Product> AddProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}
