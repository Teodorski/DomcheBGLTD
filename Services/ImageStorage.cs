namespace DomcheBGLTD.Services;

public class ImageStorage : IImageStorage
{
    private readonly IWebHostEnvironment _env;
    private const long MaxFileSize = 5 * 1024 * 1024;       // 5 MB
    private static readonly string[] Allowed = new string[] {".jpg", ".jpeg", ".png", ".webp"};

    public ImageStorage(IWebHostEnvironment env) => _env = env;

    public async Task<(bool ok, string? relativePath, string? error)> SaveAsync(IFormFile img)
    {
        if (img.Length == 0) return (false, null, "Empty file.");
        if (img.Length > MaxFileSize) return (false, null, "File too large.");

        var ext = Path.GetExtension(img.FileName).ToLowerInvariant();
        if (!Allowed.Contains(ext)) return (false, null, "Unsupported format.");

        var uploads = Path.Combine(_env.WebRootPath, "uploads");
        Directory.CreateDirectory(uploads);                 // idempotent

        var fileName = $"{Guid.NewGuid()}{ext}";
        var fullPath = Path.Combine(uploads, fileName);

        try
        {
            await using var fs = new FileStream(fullPath, FileMode.Create);
            await img.CopyToAsync(fs);
            var relPath = $"/uploads/{fileName}";
            return (true, relPath, null);
        }
        catch (Exception ex)
        {
            return (false, null, ex.Message);
        }
    }
}