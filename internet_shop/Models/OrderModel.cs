using internet_shop.Dto.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace internet_shop.Models
{
    public class OrderModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Address { get; set; }
        public DateTime Created { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

     //   [ForeignKey("Cart")]
        public int CartId { get; set; }
        // public OrderState State { get; set; }
        public OrderState State { get; set; }
    }
}
