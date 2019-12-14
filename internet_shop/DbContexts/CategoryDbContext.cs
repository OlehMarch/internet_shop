using Microsoft.EntityFrameworkCore;
using internet_shop.Models;

namespace internet_shop.DbContexts
{
    public class CategoryDbContext : BaseDbContext
    {
        public DbSet<Categories> Categories { get; set; }
    }
}
