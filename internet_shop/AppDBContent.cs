using internet_shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop
{
    public class AppDBContent : DbContext
    {
        public AppDBContent()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbName = "newcart";
            string dbUser = "root";
            string dbHost = "localhost";

            optionsBuilder.UseMySql($"server={dbHost};UserId={dbUser};database={dbName};");
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
    }
}
