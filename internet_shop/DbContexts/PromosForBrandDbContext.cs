using Microsoft.EntityFrameworkCore;
using internet_shop.Models;


namespace internet_shop.DbContexts
{
    public class PromosForBrandDbContext : BaseDbContext
    {
        public DbSet<PromosForBrandModel> PromosForBrandModel { get; set; }
    }
}
