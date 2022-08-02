using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain.Aggregates.OrderAggregate;

namespace OrderManagement.Infrastructure.Data.Contexts
{
    public class OrderManagementContext:DbContext
    {
        public OrderManagementContext(DbContextOptions options) : base(options)
        {
        
        }
        public DbSet<cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Order_Item> Order_Items { get; set; }

        protected override  void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<cart>().Property(p => p.Book_Price).HasColumnType("decimal(18,4)");
            modelBuilder.Entity<Order>().Property(p => p.Bill_Amount).HasColumnType("decimal(18,4)");
          

        }


    }
}
