using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DomcheBGLTD.Models.Helpers.Enums;

namespace DomcheBGLTD.Models.Entities;

public class Listing
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    [Required]
    public ListingType ListingType { get; set; }
    [Required]
    public int PropertyTypeId { get; set; }
    public virtual PropertyType PropertyType { get; set; }
    [Required]
    public decimal Price { get; set; }
    public int CurrencyId { get; set; } = 1; // default to BGN
    public virtual Currency? Currency { get; set; }

    public decimal? AreaM2 { get; set; }
    public int? YearBuilt { get; set; }

    [Required]
    public int CityId { get; set; }
    public virtual City? City { get; set; }
    [Required]
    public string Address1 { get; set; }
    public string? Address2 { get; set; }
    public int? Floor { get; set; }
    public int? FloorsTotal { get; set; }


    // contact
    [Required]
    public string Phone { get; set; }

    [Required]
    public string Email { get; set; }

    public string ExtraInfo { get; set; }

    // audit
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedUtc { get; set; }
    public bool IsApproved { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    [Required]
    public int ConstructionTypeId { get; set; }
    public virtual ConstructionType? ConstructionType { get; set; }


    public virtual ICollection<FeatureType> Features { get; set; } = new List<FeatureType>();  // "Furnished,Elevator,…"

    [Required]
    public string OwnerId { get; set; } = null!;
    public virtual ApplicationUser Owner { get; set; } = null!;
    public ICollection<ListingImage> Images { get; set; } = new List<ListingImage>();
}
