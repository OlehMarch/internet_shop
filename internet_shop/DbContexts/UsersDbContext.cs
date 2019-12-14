using Microsoft.EntityFrameworkCore;
using internet_shop.Entities;

namespace internet_shop.DbContexts
{
    public class UsersDbContext : BaseDbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
