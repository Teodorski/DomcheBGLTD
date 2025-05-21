using System.ComponentModel.DataAnnotations;

namespace DomcheBGLTD.Models.Entities;

public class Subscriber
{
    public int Id { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; } = default!;

    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
}
