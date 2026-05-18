namespace NexusCart.Models
{
    public class CartItemModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice
        {
            get
            {
                return Quantity * Price;
            }
        }
        public CartItemModel()
        {

        }
        public CartItemModel (ProductModel product){
            ProductId = product.Id;
            ProductName = product.Name;
            Price = product.Price;
            ImageUrl = product.ImageUrl;
        }
    }
}
