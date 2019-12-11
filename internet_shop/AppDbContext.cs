using Internet_shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_shop.Services
{
    public class AppDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbName = "product_ishop";
            string dbUser = "root";
            string dbHost = "localhost";

            optionsBuilder.UseMySql(
                $"server={dbHost};UserId={dbUser};database={dbName};"
                );
        }
        public DbSet<Product> Product { get; set; }
    }
}
