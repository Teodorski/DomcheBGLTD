using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomcheBGLTD.Models.Entities;

public class PropertyType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = null!;
    public virtual ICollection<Listing>? Listings { get; set; } = new List<Listing>();
}