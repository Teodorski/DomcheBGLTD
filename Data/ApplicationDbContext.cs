using DomcheBGLTD.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DomcheBGLTD.Data
{
    public class ApplicationDbContext
        : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Listing> Listings => Set<Listing>();
        public DbSet<ListingImage> ListingImages => Set<ListingImage>();
        public DbSet<Subscriber> Subscribers => Set<Subscriber>();
        public DbSet<ConstructionType> ConstructionTypes => Set<ConstructionType>();
        public DbSet<FeatureType> FeatureTypes => Set<FeatureType>();
        public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();
        public DbSet<Currency> Currencies => Set<Currency>();
        public DbSet<City> Cities => Set<City>();
        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            b.Entity<Listing>()
             .HasQueryFilter(l => !l.IsDeleted);
            b.Entity<ApplicationUser>()
             .HasQueryFilter(u => u.IsActive);

            b.Entity<Listing>()
             .HasMany(l => l.Features)
             .WithMany(f => f.Listings)
             .UsingEntity<Dictionary<string, object>>(
                 "ListingFeature",
                 jl => jl
                     .HasOne<FeatureType>()
                     .WithMany()
                     .HasForeignKey("FeatureTypeId")
                     .OnDelete(DeleteBehavior.Cascade),
                 jl => jl
                     .HasOne<Listing>()
                     .WithMany()
                     .HasForeignKey("ListingId")
                     .OnDelete(DeleteBehavior.Cascade),
                 jl =>
                 {
                     jl.HasKey("ListingId", "FeatureTypeId");
                     jl.ToTable("ListingFeatures");
                 });

            b.Entity<ListingImage>()
             .HasKey(li => li.Id);
            b.Entity<ListingImage>()
             .HasOne(li => li.Listing)
             .WithMany(l => l.Images)
             .HasForeignKey(li => li.ListingId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
