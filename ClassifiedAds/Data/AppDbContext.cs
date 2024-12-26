using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassifiedAds.Models;
using System.Reflection;
using System.Linq.Expressions;

namespace ClassifiedAds.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<AdImage> AdsImages { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AdvertisementGroup> AdvertisementGroups { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Lov> Lovs { get; set; }
        public DbSet<LovOption> LovOptions { get; set; }
        public DbSet<CategorySpecGroup> CategorySpecGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Category" });

            // Fluent API
            //modelBuilder.Entity<CategorySpec>().HasOne(m => m.Category).WithMany(m => m.Specs).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Comment>().HasOne(m => m.Ad).WithMany(m => m.Comments).OnDelete(DeleteBehavior.Restrict);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                //if (entityType.GetProperties().Any(p => p.Name == "RecordStatus"))
                //{
                //    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(
                //        BuildRecordStatusFilter(entityType.ClrType)
                //    );
                //}

                // Get all the foreign keys for this entity
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    // Set the delete behavior for each foreign key to Restrict
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }

                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string)) // Check if the property is of type string
                    {
                        var hasUnicodeAttribute = property.PropertyInfo.GetCustomAttribute<UnicodeDataTypeAttribute>() != null;

                        if (!hasUnicodeAttribute)
                        {
                            // Set column type as varchar
                            property.SetColumnType("varchar");
                        }

                        if (property.GetMaxLength() == null)
                        {
                            // Set length to 500 if no length is already defined
                            if (property.Name == "Token")
                            {
                                property.SetMaxLength(11);
                            }
                            else
                            {
                                property.SetMaxLength(500);
                            }
                        }
                    }
                    else if (property.ClrType == typeof(decimal) || Nullable.GetUnderlyingType(property.ClrType) == typeof(decimal))
                    {
                        // Set decimal type with 3 digits after the decimal point (e.g., decimal(18,3))
                        property.SetColumnType("decimal(18,3)");
                    }
                }

                try
                {
                    var tokenProperty = entityType.GetProperty("Token");
                    if (tokenProperty != null && tokenProperty.ClrType == typeof(string))
                    {
                        modelBuilder.Entity(entityType.ClrType)
                            .HasIndex("Token")
                            .IsUnique();
                    }
                }
                catch (Exception)
                {
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        //private LambdaExpression BuildRecordStatusFilter(Type entityType)
        //{
        //    var parameter = Expression.Parameter(entityType, "e");
        //    var property = Expression.Property(parameter, "RecordStatus");

        //    // Create expression for "RecordStatus < RecordStatus.Deleted"
        //    var deletedValue = (int)RecordStatus.Deleted; // 999
        //    var lessThanExpression = Expression.LessThan(property, Expression.Constant(deletedValue));

        //    return Expression.Lambda(lessThanExpression, parameter);
        //}
        //public override int SaveChanges()
        //{
        //    var data = ChangeTracker.Entries().Where(m => m.State == EntityState.Added);
        //    foreach (var entity in data)
        //    {
        //        if(entity is SharedModel)
        //        {

        //        }
        //    }
        //    return base.SaveChanges();
        //}
    }
}
