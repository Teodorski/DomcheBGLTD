using System.ComponentModel.DataAnnotations;
using DomcheBGLTD.Models.Helpers.Enums;
using global::DomcheBGLTD.Models.Helpers.Enums;

namespace DomcheBGLTD.Models.ViewModels;
public class EditListingVm
{
    /* ------------- basic ------------- */
    [Required, StringLength(120)] public string Title { get; set; }
    [Required, StringLength(2000)] public string Description { get; set; }
    [Required] public ListingType ListingType { get; set; }
    [Required] public PropertyType PropertyType { get; set; }

    public decimal? Price { get; set; }
    public string Currency { get; set; } = "EUR";
    public decimal? AreaM2 { get; set; }
    public int? YearBuilt { get; set; }

    /* ------------- location ------------- */
    [MaxLength(120)] public string? Address1 { get; set; }
    [MaxLength(120)] public string? Address2 { get; set; }
    public int? Floor { get; set; }
    public int? FloorsTotal { get; set; }

    /* ------------- flags ------------- */
    public string Construction { get; set; }
    public ICollection<string> Features { get; set; }

    /* ------------- contact ------------- */
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ExtraInfo { get; set; }

    /* ------------- images ------------- */
    public List<IFormFile> NewImages { get; set; } = new();
    public List<int> ExistingImageIds { get; set; } = new();


    /* ---------- helpers ---------- */
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

        Floor = Floor,
        FloorsTotal = FloorsTotal,

        Construction = Construction,
        Features = Features.ToList(),

        Phone = Phone,
        Email = Email,
        ExtraInfo = ExtraInfo,
        OwnerId = ownerId
    };
}
