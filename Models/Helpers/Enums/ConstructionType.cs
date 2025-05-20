namespace DomcheBGLTD.Models.Helpers.Enums;

[Flags]
public enum ConstructionType
{
    None = 0,
    Panel = 1 << 0,
    Brick = 1 << 1,
    EPK = 1 << 2,
    SlipForm = 1 << 3,
    BeamSystem = 1 << 4,
    Prefab = 1 << 5
}