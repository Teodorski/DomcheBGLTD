namespace DomcheBGLTD.Models.Helpers.Enums;

[Flags]
public enum ListingFeature
{
    None = 0,
    Furnished = 1 << 0,
    Elevator = 1 << 1,
    GarageIncluded = 1 << 2,
    CentralHeating = 1 << 3,
    ControlledAccess = 1 << 4,
    VideoSurveillance = 1 << 5
}