using DomcheBGLTD.Models.ViewModels;
using System.Security.Claims;

namespace DomcheBGLTD.Models.Helpers;

public static class ClaimsPrincipalExt
{
    public static string? GetUserId(this ClaimsPrincipal user) =>
        user.FindFirstValue(ClaimTypes.NameIdentifier);

    public static void MapToEntity(this CreateListingVm vm, Listing l)
    {
        l.Title = vm.Title;
        l.Description = vm.Description;
        l.Price = vm.Price ?? 0;
        l.ListingType = vm.ListingType;
        l.PropertyType = vm.PropertyType;
        l.Province = vm.Province;
        l.City = vm.City;
        l.AreaM2 = vm.AreaM2;
        l.UpdatedUtc = DateTime.UtcNow;
    }
}
