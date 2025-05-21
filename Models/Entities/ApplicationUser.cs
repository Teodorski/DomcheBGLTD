using Microsoft.AspNetCore.Identity;

namespace DomcheBGLTD.Models.Entities;

public class ApplicationUser : IdentityUser
{
    // used by Admin to ban accounts
    public bool IsActive { get; set; } = true;

    // navigation
    public virtual ICollection<Listing>? Listings { get; set; }
}