namespace internet_shop.Models
{
    public class CartProduct
    {
        public CartProduct(int productId, string cartId)
        {
            this.CartId = cartId;
            this.ProductId = productId;
        }

        public int Id { get; set; }
        public string CartId { get; set; }
        public int ProductId { get; set; }
    }
}
