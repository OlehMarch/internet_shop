﻿using Microsoft.EntityFrameworkCore;

using internet_shop.Models;

namespace internet_shop.DbContexts
{
    public class PromosDbContext : BaseDbContext
    {
        public DbSet<Promos> Promos { get; set; }
    }
}