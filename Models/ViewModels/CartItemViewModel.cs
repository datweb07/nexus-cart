namespace NexusCart.Models.ViewModels
{
    public class CartItemViewModel
    {
        public List<CartItemModel> cartItemModels { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
