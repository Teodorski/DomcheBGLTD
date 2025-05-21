using DomcheBGLTD.Models.Entities;
using DomcheBGLTD.Models.ViewModels;

namespace DomcheBGLTD.Models.Helpers;

// Helpers/ListingExtensions.cs
public static class ListingExtensions
{
    /* VM ➜ Entity (Create) */
    //public static Listing ToEntity(this CreateListingVm vm, string ownerId) => new()
    //{
    //    /* simple props */
    //    Title = vm.Title,
    //    Description = vm.Description,
    //    ListingType = vm.ListingType,
    //    PropertyType = vm.PropertyType,
    //    Price = vm.Price ?? 0,
    //    Currency = vm.Currency,
    //    AreaM2 = vm.AreaM2,
    //    YearBuilt = vm.YearBuilt,

    //    /* location */
    //    Province = vm.Province,
    //    City = vm.City,
    //    Address1 = vm.Address1,
    //    Address2 = vm.Address2,
    //    Floor = vm.Floor,
    //    FloorsTotal = vm.FloorsTotal,

    //    /* flags */
    //    Construction = vm.Construction,
    //    Features = vm.Features,

    //    /* contact */
    //    Phone = vm.Phone,
    //    Email = vm.Email,
    //    ExtraInfo = vm.AdditionalInformation,

    //    OwnerId = ownerId
    //};

    /* Entity ⇐ VM (Edit) */
    //public static void UpdateFrom(this Listing e, CreateListingVm vm)
    //{
    //    e.Title = vm.Title;
    //    e.Description = vm.Description;
    //    e.ListingType = vm.ListingType;
    //    e.PropertyType = vm.PropertyType;
    //    e.Price = vm.Price ?? 0;
    //    e.Currency = vm.Currency;
    //    e.AreaM2 = vm.AreaM2;
    //    e.YearBuilt = vm.YearBuilt;

    //    e.Province = vm.Province;
    //    e.City = vm.City;
    //    e.Address1 = vm.Address1;
    //    e.Address2 = vm.Address2;
    //    e.Floor = vm.Floor;
    //    e.FloorsTotal = vm.FloorsTotal;

    //    e.Construction = vm.Construction;
    //    e.Features = vm.Features;

    //    e.Phone = vm.Phone;
    //    e.Email = vm.Email;
    //    e.ExtraInfo = vm.AdditionalInformation;

    //    e.UpdatedUtc = DateTime.UtcNow;
    //}
}