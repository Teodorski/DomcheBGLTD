using DomcheBGLTD.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DomcheBGLTD.Models;
using DomcheBGLTD.Models.Helpers.Enums;

namespace DomcheBGLTD.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // 1. Make sure the DB & schema exist
        await ctx.Database.MigrateAsync();

        // 2. Seed a demo user if none exist
        if (!ctx.Users.Any())
        {
            var demo = new ApplicationUser
            {
                UserName = "demo@domche.bg",
                Email = "demo@domche.bg",
                PhoneNumber = "+35900000000"
            };
            await userManager.CreateAsync(demo, "P@ssword1");
        }

        // 3. Seed one listing if none exist
        if (!ctx.Listings.Any())
        {
            var owner = await ctx.Users.FirstAsync();
            ctx.Listings.Add(new Listing
            {
                Title = "Demo apartment in Sofia",
                Description = "Two-bedroom flat near Uni.",
                Price = 123000,
                AreaM2 = 82,
                ListingType = ListingType.ForSale,
                PropertyType = PropertyType.Apartment,
                Province = "Sofia-grad",
                City = "Sofia",
                IsApproved = true,
                Owner = owner
            });
            await ctx.SaveChangesAsync();
        }
    }
}
