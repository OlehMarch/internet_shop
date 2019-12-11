using internet_shop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace internet_shop.Domain
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
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

        public DbSet<Order> Orders { get; set; }
    }
}
