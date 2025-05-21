using Microsoft.Build.Framework;

namespace DomcheBGLTD.Models.Entities;

public class ConstructionType
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = null!;

    public virtual ICollection<Listing>? Constructions { get; set; } = new List<Listing>();
}