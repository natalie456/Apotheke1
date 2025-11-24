using Apotheke1.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Apotheke1.Data
{
   
        public class DbInitializer
        {
            private readonly ModelBuilder modelBuilder;

            public DbInitializer(ModelBuilder modelBuilder)
            {
                this.modelBuilder = modelBuilder;
            }

            public void Seed()
            {
                // ---- CATEGORY ----
                modelBuilder.Entity<Category>().HasData(
                    new Category { Id = 1, Name = "Знеболюючі" },
                    new Category { Id = 2, Name = "Протизастудні" },
                    new Category { Id = 3, Name = "Вітаміни" }
                );

                // ---- SUPPLIER ----
                modelBuilder.Entity<Supplier>().HasData(
                    new Supplier { Id = 1, Name = "Фарма Плюс", Phone = "380501112233", Address = "Київ, вул. Медична, 15" },
                    new Supplier { Id = 2, Name = "Аптека Лайф", Phone = "380671234567", Address = "Львів, вул. Городоцька, 44" }
                );

                // ---- MEDICINE ----
                modelBuilder.Entity<Medicine>().HasData(
                    new Medicine { Id = 1, Name = "Ібупрофен", Description = "Знеболююче та протизапальне", Price = 95, CategoryId = 1, SupplierId = 1 },
                    new Medicine { Id = 2, Name = "Парацетамол", Description = "Жарознижуючий засіб", Price = 60, CategoryId = 1, SupplierId = 2 },
                    new Medicine { Id = 3, Name = "Терафлю", Description = "Засіб від застуди", Price = 150, CategoryId = 2, SupplierId = 1 },
                    new Medicine { Id = 4, Name = "Вітамін C", Description = "Підвищує імунітет", Price = 80, CategoryId = 3, SupplierId = 2 }
                );

                // ---- CUSTOMER ----
                modelBuilder.Entity<Customer>().HasData(
                   new Customer { Id = 1, FullName = "Анна Іваненко", Email = "anna@gmail.com", Address = "Київ, вул. Хрещатик, 10", Phone = "+38095123456" },
                   new Customer { Id = 2, FullName = "Олег Петров", Email = "oleg@gmail.com", Address = "Львів, вул. Шевченка, 25", Phone = "+38095123457" }
                );

                // ---- ORDER ----
                modelBuilder.Entity<Order>().HasData(
                    new Order { Id = 1, CustomerId = 1, CreatedAt = new DateTime(2025, 10, 10), Total = 245 },
                    new Order { Id = 2, CustomerId = 2, CreatedAt = new DateTime(2025, 10, 11), Total = 150 }
                );

                // ---- ORDER ITEMS ----
                modelBuilder.Entity<OrderItem>().HasData(
                    new OrderItem { Id = 1, OrderId = 1, MedicineId = 1, Quantity = 1, Price = 95 },
                    new OrderItem { Id = 2, OrderId = 1, MedicineId = 4, Quantity = 2, Price = 80 },
                    new OrderItem { Id = 3, OrderId = 2, MedicineId = 3, Quantity = 1, Price = 150 }
                );
            }
        }
    
}
