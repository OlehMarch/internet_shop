﻿using Microsoft.EntityFrameworkCore;

using internet_shop.Models;

namespace internet_shop.DbContexts
{
    public class ProductDbContext : BaseDbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
