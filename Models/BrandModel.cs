using System.ComponentModel.DataAnnotations;

namespace NexusCart.Models
{
    public class BrandModel
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(4, ErrorMessage = "Name must be at least 4 characters long.")]
        public string ?Name { get; set; }
        [Required, MinLength(4, ErrorMessage = "Description must be at least 4 characters long.")]
        public string ?Description { get; set; }
        public string ?Slug { get; set; }
        public int Status { get; set; }
    }
}
