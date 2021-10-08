using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TestWeekADOEF.Model;

namespace TestWeekADOEF.EF.Configuration
{
    public class SpesaConfiguration : IEntityTypeConfiguration<Spesa>
    {

        public void Configure(EntityTypeBuilder<Spesa> builder)
        {
            builder.Property(s => s.Descrizione).HasMaxLength(500);
            builder.Property(s => s.Utente).HasMaxLength(100).IsRequired();
            builder.Property(s => s.Importo).HasPrecision(6,2).IsRequired();
            builder.Property(s => s.DataSpesa).IsRequired();
            builder.Property(s => s.Approvato).IsRequired();
            builder.HasOne(c => c.Category).WithMany(s => s.Spese);
        }

    }
}
