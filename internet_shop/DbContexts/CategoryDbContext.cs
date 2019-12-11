using internet_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace internet_shop.DbContexts
{
    public class CategoryDbContext : BaseDbContext
    {
        public DbSet<Categories> Categories { get; set; }
    }
}
