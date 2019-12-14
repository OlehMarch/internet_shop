using Microsoft.EntityFrameworkCore;
using internet_shop.Models;


namespace internet_shop.DbContexts
{
    public class PromosForProductDbContext : BaseDbContext
    {
        public DbSet<PromosForProductModel> PromosForProductModel { get; set; }
    }
}
