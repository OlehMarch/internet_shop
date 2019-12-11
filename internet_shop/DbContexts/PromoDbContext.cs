using internet_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace internet_shop.DbContexts
{
    public class PromoDbContext : BaseDbContext
    {
        public DbSet<Promos> Promos { get; set; }
    }
}
