namespace internet_shop.Models
{
    public class Cart
    {
        public Cart() { }

        public Cart(string cartId)
        {
            CartId = cartId;
        }

        public string CartId { get; set; }
        public string ProfileId { get; set; }
        public string Address { get; set; }
    }
}
