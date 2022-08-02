using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Aggregates.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManagement.Infrastructure.Data.Config
{
    public class CartEntityTypeConfiguration : IEntityTypeConfiguration<cart>
    {
        public void Configure(EntityTypeBuilder<cart> builder)
        {
            builder.Property(p => p.Book_Name).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.Book_Type).IsRequired(true).HasMaxLength(50);
            builder.Property(p => p.Book_Price).IsRequired(true).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Quantity).IsRequired(true).HasMaxLength(50);

        }
    }
}