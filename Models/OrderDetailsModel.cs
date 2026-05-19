using System.ComponentModel.DataAnnotations.Schema;

namespace NexusCart.Models
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OrderCode { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("ProductId")]
        public ProductModel Product { get; set; }
    }
}
