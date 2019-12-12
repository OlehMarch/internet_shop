using internet_shop.Models;
using Microsoft.EntityFrameworkCore;

namespace internet_shop.DbContexts
{
    public class ProfileDbContext : BaseDbContext
    {
        public DbSet<Profile> Profiles { get; set; }
    }
}

