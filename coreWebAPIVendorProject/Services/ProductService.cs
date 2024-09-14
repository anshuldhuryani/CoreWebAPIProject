using coreWebAPIVendorProject.Models;
using coreWebAPIVendorProject.Repositories;

namespace coreWebAPIVendorProject.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            this._repository = repository;
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            return await _repository.AddProductAsync(product);
        }
        public async Task DeleteProductAsync(int id)
        {
            await _repository.DeleteProductAsync(id);
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllProductsAsync();
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _repository.GetProductByIdAsync(id);
        }
        public async Task<Product> UpdateProductAsync(Product product)
        {
            return await _repository.UpdateProductAsync(product);
        }
    }
}
