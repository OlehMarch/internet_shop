using Microsoft.EntityFrameworkCore;
using internet_shop.Models;


namespace internet_shop.DbContexts
{
    public class PromosForCategoriesDbContext : BaseDbContext
    {
        public DbSet<PromosForCategoriesModel> PromosForCategoriesModel { get; set; }
    }
}
