using coreWebAPIBasicProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreWebAPIBasicProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Static List of Product:
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200 },
            new Product { Id = 2, Name = "Phone", Price = 1500 },
            new Product { Id = 3, Name = "Printer", Price = 1500 },
            new Product { Id = 4, Name = "Camera", Price = 2200 },
            new Product { Id = 5, Name = "Laptop", Price = 1200 },
            new Product { Id = 6, Name = "Phone", Price = 1500 },
            new Product { Id = 7, Name = "Printer", Price = 1500 },
            new Product { Id = 8, Name = "Camera", Price = 2200 },
            new Product { Id = 9, Name = "Laptop", Price = 1200 },
            new Product { Id = 10, Name = "Phone", Price = 1500 },
            new Product { Id = 11, Name = "Printer", Price = 1500 },
            new Product { Id = 12, Name = "Camera", Price = 2200 },
            new Product { Id = 13, Name = "Laptop", Price = 1200 },
            new Product { Id = 14, Name = "Phone", Price = 1500 },
            new Product { Id = 15, Name = "Printer", Price = 1500 },
            new Product { Id = 16, Name = "Camera", Price = 2200 },
            new Product { Id = 17, Name = "Laptop", Price = 1200 },
            new Product { Id = 18, Name = "Phone", Price = 1500 },
            new Product { Id = 19, Name = "Printer", Price = 1500 },
            new Product { Id = 20, Name = "Camera", Price = 2200 },
        };

        [HttpGet]
        public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber = 1, int pageSize = 3)
        {
            var paginatedProducts = await Task.Run(() => products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
            return paginatedProducts;
        }
    }
}
