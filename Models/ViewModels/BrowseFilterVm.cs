using DomcheBGLTD.Models.Helpers.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomcheBGLTD.Models.ViewModels;

// ViewModels/BrowseFilterVm.cs
public class BrowseFilterVm
{
    public ListingType? Listing { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    public decimal? MinAreaM2 { get; set; }
    public decimal? MaxAreaM2 { get; set; }

    public int? Floor { get; set; }
    public int? FloorsTotal { get; set; }

    public bool HasPictures { get; set; } = false;

    public int? CityId { get; set; }
    public List<int> SelectedFeatures { get; set; } = [];
    public List<int> SelectedPropertyTypes { get; set; } = [];
    public List<int> SelectedConstructionTypes { get; set; } = [];

    public List<SelectListItem> Cities { get; set; } = new(); // multi-select
    public IEnumerable<SelectListItem> PropertyTypes { get; set; } = [];
    public IEnumerable<SelectListItem> ConstructionTypes { get; set; } = [];
    public IEnumerable<SelectListItem> AvailableFeatures { get; set; } = [];

    public IEnumerable<ListingCardVm>? Results { get; set; }
}
