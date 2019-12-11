using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Domain.Entity
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
