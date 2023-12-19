﻿using Microsoft.EntityFrameworkCore;
using OrderEntity = Order.Service.DataAccess.Entities.Order;

namespace Order.Service.DataAccess.Contexts
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Console.WriteLine, LogLevel.Information)
                .UseSnakeCaseNamingConvention();
        }

        public DbSet<OrderEntity> Orders { get; set; }
    }
}
