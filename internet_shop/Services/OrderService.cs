using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

using internet_shop.Models;
using internet_shop.Entities;
using internet_shop.DbContexts;

namespace internet_shop.Services
{
    public class OrderService
    {
        public OrderService(OrderDbContext context)
        {
            this.context = context;
        }

        private readonly OrderDbContext context;

        private OrderModel Map(Order order)
            => new OrderModel
            {
                Id = order.Id,
                Address = order.Address,
                Created = order.Created.ToString(),
                Total = order.Total,
                ClientId = order.ClientId,
                CartId = order.CartId
            };

        private Order Map(OrderModel orderModel)
            => new Order
            {
                Id = orderModel.Id,
                Address = orderModel.Address,
                Created = DateTime.Parse(orderModel.Created),
                Total = orderModel.Total,
                ClientId = orderModel.ClientId,
                CartId = orderModel.CartId
            };

        private IReadOnlyCollection<OrderModel> GetMap(IReadOnlyCollection<Order> orders)
            => orders.Select(GetMap).ToList();

        private OrderModel GetMap(Order order)
        {
            return new OrderModel()
            {
                Id = order.Id,
                Address = order.Address,
                Created = order.Created.ToString(),
                Total = order.Total,
                ClientId = order.ClientId,
                CartId = order.CartId
            };
        }

        public async Task<IReadOnlyCollection<OrderModel>> GetAsync() => GetMap(await context.Orders.ToListAsync());

        public async Task<OrderModel> GetAsync(int id) => GetMap(await context.Orders.FindAsync(id));

        public async Task<OrderModel> AddAsync(OrderModel orderData)
        {
            var addingResult = await context.Orders.AddAsync(Map(orderData));
            context.SaveChanges();
            return Map(addingResult.Entity);
        }
    }
}
