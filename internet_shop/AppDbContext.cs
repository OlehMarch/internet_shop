using internet_shop.Models;
using Microsoft.EntityFrameworkCore;
using System;


namespace internet_shop
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
        public DbSet<Promos> Promos { get; set; }
    }
}
