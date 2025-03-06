using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoilerPlate.Entity.Entities.Abstract;
using BoilerPlate.Entity.Entities.Concrete;
using BoilerPlate.Entity.Entities.Concrete.File;
using BoilerPlate.Entity.Entities.Concrete.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.DAL.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BaseFile> Files { get; set; }
        public DbSet<ImageFile> ImageFiles { get; set; }
        public DbSet<DocumentFile> DocumentFiles { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TPH yapılandırması
            modelBuilder.Entity<BaseFile>()
                .HasDiscriminator<string>("FileType")
                .HasValue<ImageFile>("Image")
                .HasValue<DocumentFile>("Document");

            // BlogImage ara tablo yapılandırması
            modelBuilder.Entity<BlogImage>()
                .HasKey(bi => new { bi.BlogId, bi.ImageId });

            modelBuilder.Entity<BlogImage>()
                .HasOne(bi => bi.Blog)
                .WithMany(b => b.BlogImages)
                .HasForeignKey(bi => bi.BlogId);

            modelBuilder.Entity<BlogImage>()
                .HasOne(bi => bi.Image)
                .WithMany(i => i.BlogImages)
                .HasForeignKey(bi => bi.ImageId);

            // BlogCategory ara tablo yapılandırması
            modelBuilder.Entity<BlogCategory>()
                .HasKey(bc => new { bc.BlogId, bc.CategoryId });

            modelBuilder.Entity<BlogCategory>()
                .HasOne(bc => bc.Blog)
                .WithMany(b => b.BlogCategories)
                .HasForeignKey(bc => bc.BlogId);

            modelBuilder.Entity<BlogCategory>()
                .HasOne(bc => bc.Category)
                .WithMany(c => c.BlogCategories)
                .HasForeignKey(bc => bc.CategoryId);
        }

        // SaveChanges icin CreatedDate ve ModifiedDate i otomatik olarak yerlestiren interceptor
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entityler uzerinde yapilan degisikliklerin ya da yeni eklenen verinin yakalnmasini saglayan propertydir. Update operasyonlarinda track edilen verileri yakalayip elde etmemizi saglar
            var entries = ChangeTracker.Entries<BaseEntity>();
            foreach (var entry in entries)
            {
                TimeZoneInfo turkeyTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Turkey Standard Time");
                DateTime istanbulTime = TimeZoneInfo.ConvertTime(DateTime.Now, turkeyTimeZone);
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = istanbulTime;
                    entry.Entity.ModifiedDate = istanbulTime;
                }
                if (entry.State == EntityState.Modified)
                    entry.Entity.ModifiedDate = istanbulTime;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
