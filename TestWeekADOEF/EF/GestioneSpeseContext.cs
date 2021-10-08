using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TestWeekADOEF.EF.Configuration;
using TestWeekADOEF.Model;

namespace TestWeekADOEF.EF
{
    public class GestioneSpeseContext : DbContext
    {
        public GestioneSpeseContext() : base() { }

        public GestioneSpeseContext(DbContextOptions<GestioneSpeseContext> options)
            : base(options) { }

        public DbSet<Spesa> Spese { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                string connectionStringSQL = config.GetConnectionString("AcademyG");

                optionsBuilder.UseSqlServer(connectionStringSQL);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SpesaConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
