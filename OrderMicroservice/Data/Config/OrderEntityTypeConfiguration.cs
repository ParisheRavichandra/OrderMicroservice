using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Aggregates.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Infrastructure.Data.Config
{
    class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(p => p.Order_Date).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.User_Id).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.Bill_Amount).IsRequired(true).HasColumnType("decimal(18,2)");
          
            builder.Property(p => p.Book_Name).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.Quantity).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.Book_Type).IsRequired(true).HasMaxLength(50);
          
        }
    }
}