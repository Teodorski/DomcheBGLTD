using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DomcheBGLTD.Models.Helpers.Enums;

namespace DomcheBGLTD.Models;

public class Listing
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ListingType ListingType { get; set; }
    public PropertyType PropertyType { get; set; }
    public decimal Price { get; set; }
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

    // new multi-selects
    public ConstructionType Construction { get; set; }
    public ListingFeature Features { get; set; }

    // contact
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public string? ExtraInfo { get; set; }

    // audit
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedUtc { get; set; }
    public bool IsApproved { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    // navigation
    public string OwnerId { get; set; } = null!;
    public ApplicationUser Owner { get; set; } = null!;
    public ICollection<ListingImage> Images { get; set; } = new List<ListingImage>();
}
