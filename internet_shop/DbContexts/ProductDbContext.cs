using Internet_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace internet_shop
{
    public class ProductDbContext : BaseDbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
