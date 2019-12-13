using Microsoft.EntityFrameworkCore;
using internet_shop.Models;
using internet_shop.Entities;

namespace internet_shop.DbContexts
{
    public class ProductDbContext : BaseDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Promos> Promos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
