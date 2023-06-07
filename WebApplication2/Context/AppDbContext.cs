using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WarehouseWeb.Model;

namespace WarehouseWeb.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(x => x.Company)
                .WithMany()
                .HasForeignKey(x => x.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StorageItem>()
                .HasOne(x => x.Product)
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StorageItem>()
                .HasOne(x => x.Storage)
                .WithMany(g => g.StorageItemList)
                .HasForeignKey(x => x.StorageId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StorageItem>()
                .HasMany(x => x.ListOfStorageItemInputOutputs)
                .WithOne(g => g.StorageItem)
                .HasForeignKey(x => x.StorageItemId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.User)
                .WithMany(g => g.UserRoleList)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(g => g.UserRoleList)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoleClaims>()
                .HasOne(x => x.Activities)
                .WithMany(g => g.ActivitiesRoleList)
                .HasForeignKey(x => x.ActivitiesId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RoleClaims>()
                .HasOne(x => x.Role)
                .WithMany(g => g.ActivitiesRoleList)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ClassificationSpecification>()
                .HasMany(g => g.ClassificationValuesList)
                .WithOne(v => v.ClassificationSpecification)
                .HasForeignKey(v => v.ClassificationSpecificationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.Supplier)
                .WithMany()
                .HasForeignKey(x => x.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x => x.Order)
                .WithMany(g => g.OrderItemList)
                .HasForeignKey(g => g.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(x => x.ClassificationValue)
                .WithMany()
                .HasForeignKey(x => x.ClassificationValueId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<ClassificationValue>()
            //    .HasMany(g => g.ListOfQuanties)
            //    .WithOne(v => v.ClassificationValue)
            //    .HasForeignKey(v => v.ClassificationValueId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<Quantity>()
            //    .HasOne(x => x.ClassificationValue)
            //    .WithMany()
            //   .HasForeignKey(g => g.ClassificationValueId)
            //    .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<StorageItem>()
                .OwnsOne(x => x.QuantityAmount);

            modelBuilder.Entity<OrderItem>()
                .OwnsOne(x => x.QuantityAmount);

            modelBuilder.Entity<StorageItemInputOutput>()
                .OwnsOne(x => x.QuantityAmount);
            

        }

        public DbSet<User> User { get; set; }
        public DbSet<Claims> Activities { get; set; }
        public DbSet<RoleClaims> ActivitiesRole { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Storage> Storage { get; set; }
        public DbSet<StorageItem> StorageItem { get; set; }
        public DbSet<StorageItemInputOutput> StorageItemInputOutput { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<ClassificationSpecification> ClassificationSpecification { get; set; }
        public DbSet<ClassificationValue> ClassificationValue { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Supplier> Supplier { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-FABS0TF\\SQLEXPRESS;Database=warehouseweb;Trusted_Connection=True;MultipleActiveResultSets=true");
                base.OnConfiguring(optionsBuilder);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
