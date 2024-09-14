using coreWebAPIBasicProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace coreWebAPIBasicProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
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

        /*[HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return products;
        }*/

        // Request URL: http://localhost:5176/api/product?pageNumber=1&pageSize=2
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts(int pageNumber = 1, int pageSize = 3)
        {
            // Ensure pageNumber and pageSizehas valid values
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return BadRequest("Page number and page size must be greater than zero.");
            }
            // Calculate total number of items and total pages
            var totalItems = products.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            // Check if requested page exists
            if(pageNumber > totalPages)
            {
                return NotFound("Page Not found.");
            }
            // Fetch the paginated list of products
            var paginatedProducts = products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            // Create a pagination metadata object (Optional)
            var paginationMetadata = new
            {
                totalItems = totalItems,
                pageSize = pageSize,
                CurrentPage = pageNumber,
                totalPages = totalPages,
                HasPreviousPage = pageNumber > 1,
                HasNextPage = pageNumber < totalPages,
            };
            Response.Headers["X-Pagination"] = System.Text.Json.JsonSerializer.Serialize(paginationMetadata);
            return Ok(paginatedProducts);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Product>> searchProducts(string? name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var filteredProducts = products.AsEnumerable();

            if (!string.IsNullOrEmpty(name))
            {
                filteredProducts = filteredProducts.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            if (minPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price >= minPrice.Value);
            }
            if (maxPrice.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price <= maxPrice.Value);
            }

            return Ok(filteredProducts.ToList());
        }

        [HttpGet("searchagain")]
        public ActionResult<IEnumerable<Product>> searchProductsAgain(string? name = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            var filteredProducts = new List<Product>();
            foreach (var product in products)
            {
                bool matches = true;
                if (!string.IsNullOrEmpty(name) && !product.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    matches = false;
                }
                if (minPrice.HasValue && product.Price < minPrice.Value)
                {
                    matches = false;
                }
                if (maxPrice.HasValue && product.Price > maxPrice.Value)
                {
                    matches = false;
                }
                if (matches)
                {
                    filteredProducts.Add(product);
                }
            }
            return Ok(filteredProducts.ToList());   
        }

        [HttpGet("sorted")]
        public ActionResult<IEnumerable<Product>> GetSortedProducts(string sortBy = "name")
        {
            var sortedProducts = sortBy.ToLower() switch
            {
                "price" => products.OrderBy(p => p.Price).ToList(),
                _ => products.OrderBy(p => p.Name).ToList()
            };
            return Ok(sortedProducts.ToList());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProductById(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
            // Assigns a new Id to Product Object
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            /*return Ok(product);*/
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, Product product)
        {
            var existingProduct = products.FirstOrDefault(product => product.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return NoContent();
        }

    }
}
