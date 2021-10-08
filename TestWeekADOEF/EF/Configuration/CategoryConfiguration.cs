using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestWeekADOEF.Model;

namespace TestWeekADOEF.EF.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {

        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Categoria).HasMaxLength(100).IsRequired();
            builder.HasMany(s => s.Spese).WithOne(c => c.Category);
        }

    }
}
