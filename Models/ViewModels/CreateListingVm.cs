using System.ComponentModel.DataAnnotations;
using DomcheBGLTD.Models.Helpers.Enums;

namespace DomcheBGLTD.Models.ViewModels;
public class CreateListingVm
{
    [Required, StringLength(120)] public string Title { get; set; }
    [Required, StringLength(2000)] public string Description { get; set; }

    [Required] public ListingType ListingType { get; set; }
    [Required] public PropertyType PropertyType { get; set; }
    public decimal? Price { get; set; }
    public string Currency { get; set; } = "EUR";
    public decimal? AreaM2 { get; set; }
    public int? YearBuilt { get; set; }

    // location
    public string? Province { get; set; }
    public string? City { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public int? Floor { get; set; }
    public int? FloorsTotal { get; set; }

    // multi-selects
    public ConstructionType Construction { get; set; }
    public ListingFeature Features { get; set; }

    // contact
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ExtraInfo { get; set; }

    // images
    public List<IFormFile> Images { get; set; } = new();

    /* helper: VM -> entity */
    public Listing ToEntity(string ownerId) => new()
    {
        Title = Title,
        Description = Description,
        ListingType = ListingType,
        PropertyType = PropertyType,
        Price = Price ?? 0,
        Currency = Currency,
        AreaM2 = AreaM2,
        YearBuilt = YearBuilt,

        Province = Province,
        City = City,
        Address1 = Address1,
        Address2 = Address2,
        Floor = Floor,
        FloorsTotal = FloorsTotal,

        Construction = Construction,
        Features = Features,

        Phone = Phone,
        Email = Email,
        ExtraInfo = ExtraInfo,

        OwnerId = ownerId
    };
}
