using Microsoft.EntityFrameworkCore;

using internet_shop.Entities;

namespace internet_shop.DbContexts
{
    public class OrderDbContext : BaseDbContext
    {
        public DbSet<Order> Orders { get; set; }
    }
}
