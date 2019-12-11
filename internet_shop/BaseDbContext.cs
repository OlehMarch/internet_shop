using internet_shop.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
