using Microsoft.EntityFrameworkCore;

using internet_shop.Models;

namespace internet_shop.DbContexts
{
    public class ProfileDbContext : BaseDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
    }
}

