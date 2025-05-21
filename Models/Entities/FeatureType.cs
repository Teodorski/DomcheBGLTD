using System.ComponentModel.DataAnnotations;

namespace DomcheBGLTD.Models.Entities;

public class FeatureType
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    public virtual ICollection<Listing>? Listings { get; set; } = new List<Listing>();
}