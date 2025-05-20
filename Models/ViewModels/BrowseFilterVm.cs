using DomcheBGLTD.Models.Helpers.Enums;

namespace DomcheBGLTD.Models.ViewModels;

// ViewModels/BrowseFilterVm.cs
public class BrowseFilterVm
{
    public ListingType? Listing { get; set; }                 // ForSale / ForRent
    public List<PropertyType> Property { get; set; } = new();  // multi-select
    public string? City { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public IEnumerable<ListingCardVm>? Results { get; set; }
}
