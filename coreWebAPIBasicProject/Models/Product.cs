using System.ComponentModel.DataAnnotations;

namespace coreWebAPIBasicProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name is Required.")]
        [StringLength(20, ErrorMessage = "Product name cannot exceed 20 characters.")]
        public string? Name { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
    }
}
