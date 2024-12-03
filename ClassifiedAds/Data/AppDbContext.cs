using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClassifiedAds.Models;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<CategorySpec>().HasOne(m => m.Category).WithMany(m => m.Specs).OnDelete(DeleteBehavior.Restrict);
            //modelBuilder.Entity<Comment>().HasOne(m => m.Ad).WithMany(m => m.Comments).OnDelete(DeleteBehavior.Restrict);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // Get all the foreign keys for this entity
                foreach (var foreignKey in entityType.GetForeignKeys())
                {
                    // Set the delete behavior for each foreign key to Restrict
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            base.OnModelCreating(modelBuilder);
        }

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
