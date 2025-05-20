using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomcheBGLTD.Models;

public class ListingImage
{
    public int Id { get; set; }
    public int ListingId { get; set; }

    // the raw file bytes
    [Column(TypeName = "varbinary(max)")]
    public byte[] Data { get; set; } = default!;

    // for response headers
    [MaxLength(80)]
    public string ContentType { get; set; } = default!;   // "image/jpeg", …

    public int Order { get; set; }
}
