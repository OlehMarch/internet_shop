using Microsoft.EntityFrameworkCore;

using internet_shop.Models;

namespace internet_shop.DbContexts
{
    public class BrandDbContext : BaseDbContext
    {
        public DbSet<Brand> Brands { get; set; }
    }
}
