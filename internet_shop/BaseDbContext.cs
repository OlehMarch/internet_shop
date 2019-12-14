using Microsoft.EntityFrameworkCore;

using internet_shop.Models;
using internet_shop.Entities;

namespace internet_shop
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbName = "internet_shop";
            string dbUser = "root";
            string dbHost = "localhost";

            optionsBuilder.UseMySql(
                $"server={dbHost};UserId={dbUser};database={dbName};"
            );
        }

        //public DbSet<Cart> Carts { get; set; } // TODO(artemmal): DB Key too long, cannot create db
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Promos> Promos { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
    }
}
