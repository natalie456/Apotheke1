using Apotheke1.Entity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Apotheke1.Data
{
    public class ApothekeDbContext : IdentityDbContext<IdentityUser>
    {
        public ApothekeDbContext(DbContextOptions<ApothekeDbContext> options) : base(options) { }


        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> Suppliers => Set<Supplier>();
        public DbSet<Medicine> Medicines => Set<Medicine>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Medicine>()
            .HasOne(m => m.Category)
            .WithMany(c => c.Medicines)
            .HasForeignKey(m => m.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Medicine>()
            .HasOne(m => m.Supplier)
            .WithMany(s => s.Medicines)
            .HasForeignKey(m => m.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Medicine>()
        .Property(m => m.Price)
        .HasPrecision(18, 2);

            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasPrecision(18, 2);

            base.OnModelCreating(modelBuilder);

            // Виклик seed-даних
            new DbInitializer(modelBuilder).Seed();
        }
    }
  
}
