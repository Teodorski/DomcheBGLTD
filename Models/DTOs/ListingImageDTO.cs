namespace DomcheBGLTD.Models.DTOs;

public class ListingImageDto
{
    public int Id { get; set; }
    public string ContentType { get; set; } = "";
    public byte[] Data { get; set; } = [];
}