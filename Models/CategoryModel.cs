using System.ComponentModel.DataAnnotations;

namespace NexusCart.Models
{
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name must be at least 4 characters long.")]
        public string ?Name { get; set; }
        [Required(ErrorMessage = "Description must be at least 4 characters long.")]
        public string ?Description { get; set; }
        public string ?Slug { get; set; }
        public int Status { get; set; }
    }
}
