using DomcheBGLTD.Models;
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

        // add global filters for soft-delete & IsActive here if you like
        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            b.Entity<Listing>().HasQueryFilter(l => !l.IsDeleted);
            b.Entity<ApplicationUser>().HasQueryFilter(u => u.IsActive);
        }
    }
}
