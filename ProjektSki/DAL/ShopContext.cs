using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ProjektSki.Models;
using System.Threading.Tasks;

namespace ProjektSki.Data
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options) : base(options) { }

        public DbSet<ProjektSki.Models.Product> Product { get; set; }
        public DbSet<ProjektSki.Models.Category> Category { get; set; }
        public DbSet<ProjektSki.Models.Producer> Producer_1 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjektSki.Models.Product>();
            modelBuilder.Entity<ProjektSki.Models.Category>();
            modelBuilder.Entity<ProjektSki.Models.Producer>();
        }
         
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Waterski");
        }

    }
}
