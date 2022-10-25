using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyStockApp.Core.Domain.Common;
using MyStockApp.Core.Domain.Entities;

namespace MyStockApp.Infrastucture.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Document> Documents { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region tables

            modelBuilder.Entity<Product>()
                .ToTable("Products");

            modelBuilder.Entity<Category>()
                .ToTable("Categories");

            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<Document>()
                .ToTable("Documents");

            #endregion

            #region primaryKeys
            modelBuilder.Entity<Product>()
                .HasKey(product => product.Id);

            modelBuilder.Entity<Category>()
                .HasKey(category => category.Id);

            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<Document>()
                .HasKey(doc => doc.Id);
            #endregion

            #region relationships
            modelBuilder.Entity<Category>()
                .HasMany<Product>(category => category.Products)
                .WithOne(product => product.Category)
                .HasForeignKey(product => product.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
            .HasMany<Product>(user => user.Products)
            .WithOne(product => product.User)
            .HasForeignKey(product => product.UserId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
            .HasMany<Document>(proc => proc.Documents)
            .WithOne(doc => doc.Product)
            .HasForeignKey(product => product.ProductID)
            .OnDelete(DeleteBehavior.Cascade);

            #endregion

            #region property configurations

            #region products

            modelBuilder.Entity<Product>().
                Property(product => product.Name)
                .IsRequired();

            modelBuilder.Entity<Product>().
               Property(product => product.Price)
               .IsRequired();

            #endregion

            #region categories
            modelBuilder.Entity<Category>().
              Property(category => category.Name)
              .IsRequired()
              .HasMaxLength(100);
            #endregion

            #region users

            modelBuilder.Entity<User>().
                Property(user => user.Name)
                .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Username)
               .IsRequired();

            modelBuilder.Entity<User>().
              Property(user => user.Password)
              .IsRequired();

            modelBuilder.Entity<User>().
              Property(user => user.Email)
              .IsRequired();

            modelBuilder.Entity<User>().
               Property(user => user.Phone)
               .IsRequired();

            #endregion

            #endregion

        }
    }
}
