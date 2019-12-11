using internet_shop.Dto.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Models.Order
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Created { get; set; }
        public decimal Total { get; set; }
        public int ClientId { get; set; }
        public int CartId { get; set; }
       // public OrderState State { get; set; }
    }
}
