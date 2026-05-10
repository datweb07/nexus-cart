using System.ComponentModel.DataAnnotations;

namespace NexusCart.Repository.Validation
{
    public class FileAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                //kiểm tra xem file có phải là hình ảnh hay không
                //if (file.ContentType.StartsWith("image/"))
                //{
                //    return ValidationResult.Success;
                //}
                //else
                //{
                //    return new ValidationResult("Only image files are allowed.");
                //}

                var extension = Path.GetExtension(file.FileName).ToLower();
                string [] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                bool result = allowedExtensions.Any(x => extension.EndsWith(x));
                if (!result)
                {
                    return new ValidationResult("Only image files are allowed.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
