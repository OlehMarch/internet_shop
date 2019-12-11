using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace internet_shop.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        [ForeignKey("Product")]
        public int CartId { get; set; }
    }
}
