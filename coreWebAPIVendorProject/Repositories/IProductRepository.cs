using coreWebAPIVendorProject.Models;

namespace coreWebAPIVendorProject.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<Product> UpdateProductAsync(Product product);

    }
}
