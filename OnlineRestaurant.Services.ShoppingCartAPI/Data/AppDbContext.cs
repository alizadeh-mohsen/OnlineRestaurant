﻿using Microsoft.EntityFrameworkCore;
using OnlineRestaurant.Services.ShoppingCartAPI.Models;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<CartHeader> CartHeaders{ get; set; } 
        public DbSet<CartDetail> CartDetails{ get; set; }

    }
}
