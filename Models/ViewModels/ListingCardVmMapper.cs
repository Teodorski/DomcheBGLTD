namespace DomcheBGLTD.Models.ViewModels;

public static class ListingCardVmMapper
{
    public static ListingCardVm From(this Listing l) =>
        new(l.Id, l.Title, l.Price,
            l.Images.FirstOrDefault()?.Id ?? 0);
}