using DomcheBGLTD.Models.Entities;
using DomcheBGLTD.Models.ViewModels;
using System.Security.Claims;

namespace DomcheBGLTD.Models.Helpers;

public static class ClaimsPrincipalExt
{
    public static string? GetUserId(this ClaimsPrincipal user) =>
        user.FindFirstValue(ClaimTypes.NameIdentifier);
}
