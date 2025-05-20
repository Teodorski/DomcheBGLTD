using DomcheBGLTD.Models.ViewModels;

namespace DomcheBGLTD.Models.Helpers;

// Helpers/ListingExtensions.cs
public static class ListingExt
{
    public static void UpdateFrom(this Listing l, CreateListingVm vm)
    {
        l.Title = vm.Title;
        l.Description = vm.Description;
        l.ListingType = vm.ListingType;
        l.PropertyType = vm.PropertyType;
        l.Price = vm.Price ?? 0;
        l.Currency = vm.Currency;
        l.AreaM2 = vm.AreaM2;
        l.YearBuilt = vm.YearBuilt;

        l.Province = vm.Province;
        l.City = vm.City;
        l.Address1 = vm.Address1;
        l.Address2 = vm.Address2;
        l.Floor = vm.Floor;
        l.FloorsTotal = vm.FloorsTotal;

        l.Construction = vm.Construction;
        l.Features = vm.Features;

        l.Phone = vm.Phone;
        l.Email = vm.Email;
        l.ExtraInfo = vm.ExtraInfo;
    }
}
