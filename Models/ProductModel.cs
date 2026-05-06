using System.ComponentModel.DataAnnotations;

namespace NexusCart.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Name must be at least 4 characters long.")]
        public string ?Name { get; set; }
        public string ?Slug { get; set; }
        [Required, MinLength(4, ErrorMessage = "Description must be at least 4 characters long.")]
        public string ?Description { get; set; }
        [Required, MinLength(4, ErrorMessage = "Price must be at least 4 characters long.")]
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public CategoryModel ?Category { get; set; }
        public BrandModel ?Brand { get; set; }
        public string ?ImageUrl { get; set; }
    }
}
