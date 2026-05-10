using NexusCart.Repository.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexusCart.Models
{
    public class ProductModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm!")]
        public string ?Name { get; set; }

        public string ?Slug { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mô tả sản phẩm!")]
        public string ?Description { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá sản phẩm lớn hơn 0!")]
        [Range(0.01, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn thương hiệu!"), Range(1, int.MaxValue)]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục!"), Range(1, int.MaxValue)]
        public int CategoryId { get; set; }

        public CategoryModel ?Category { get; set; }

        public BrandModel ?Brand { get; set; }

        public string ?ImageUrl { get; set; }

        [NotMapped]
        [File]
        public IFormFile ?ImageFile { get; set; }
    }
}
