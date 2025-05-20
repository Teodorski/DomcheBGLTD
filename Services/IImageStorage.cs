namespace DomcheBGLTD.Services;

public interface IImageStorage
{
    Task<(bool ok, string? relativePath, string? error)> SaveAsync(IFormFile img);
}