using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer; 
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class ContextObl : DbContext
    {
        public DbSet<TouristSpot> TouristSpots { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<CategoryTouristSpot> CategoriesTouristSpots { get; set; }

        public DbSet<Lodging> Lodgings { get; set;}

        public DbSet<Reserve> Reserves { get; set;}

        public DbSet<User> Users { get; set; }

        public DbSet<UserSession> UserSessions { get; set; }

        public ContextObl(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryTouristSpot>()
                .HasKey(cts => new { cts.CategoryId, cts.TouristSpotId });

            modelBuilder.Entity<CategoryTouristSpot>()
                .HasOne<TouristSpot>(ts => ts.TouristSpot)
                .WithMany(ts => ts.ListOfCategories)
                .HasForeignKey(ts => ts.TouristSpotId);

            modelBuilder.Entity<CategoryTouristSpot>()
                .HasOne(cat => cat.Category)
                .WithMany(cat => cat.ListOfTouristSpot)
                .HasForeignKey(cat => cat.CategoryId);

        }

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
