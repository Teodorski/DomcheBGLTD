using System.ComponentModel.DataAnnotations;
using DomcheBGLTD.Models.Entities;
using DomcheBGLTD.Models.Helpers.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DomcheBGLTD.Models.ViewModels;
public class CreateListingVm
{
    [Required, StringLength(120)]
    public string Title { get; set; }

    [Required, StringLength(2000)]
    public string Description { get; set; }

    [Required]
    public ListingType ListingType { get; set; }

    [Required]
    public int PropertyTypeId { get; set; }

    [Required]
    public decimal Price { get; set; }
    public int CurrencyId { get; set; }
    public decimal? AreaM2 { get; set; }
    public int? YearBuilt { get; set; }

    // location
    [Required]
    public int CityId { get; set; }

    [Required]
    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public int? Floor { get; set; }
    public int? FloorsTotal { get; set; }

    // multi-selects
    public int ConstructionTypeId { get; set; }
    public List<int> Features { get; set; } = new List<int>();
    public IEnumerable<SelectListItem> PropertyTypes { get; set; } = [];
    public IEnumerable<SelectListItem> Cities { get; set; } = [];
    public IEnumerable<SelectListItem> Currencies { get; set; } = [];
    public IEnumerable<SelectListItem> ConstructionTypes { get; set; } = [];
    public IEnumerable<SelectListItem> AvailableFeatures { get; set; } = [];


    // contact
    [Required]
    public string Phone { get; set; }
    [Required]
    public string Email { get; set; }
    [Required, StringLength(2000)]
    public string? AdditionalInformation { get; set; }

    // images
    public List<IFormFile> Images { get; set; } = new();

    public Listing ToEntity(string ownerId, List<FeatureType> featureTypes)
    {
        var listing = new Listing
        {
            Title = Title,
            Description = Description,
            ListingType = ListingType,
            PropertyTypeId = PropertyTypeId,
            Price = Price,
            CurrencyId = CurrencyId,
            AreaM2 = AreaM2,
            YearBuilt = YearBuilt,

            CityId = CityId,
            Address1 = Address1,
            Address2 = Address2,
            Floor = Floor,
            FloorsTotal = FloorsTotal,

            ConstructionTypeId = ConstructionTypeId,

            Phone = Phone,
            Email = Email,
            ExtraInfo = AdditionalInformation,

            OwnerId = ownerId,

            // skip-nav: attach the FeatureType entities you looked up in the controller
            Features = featureTypes
        };

        return listing;
    }
}
