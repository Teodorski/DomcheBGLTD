using DomcheBGLTD.Data;
using DomcheBGLTD.Models;
using DomcheBGLTD.Models.Helpers;
using DomcheBGLTD.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DomcheBGLTD.Services;

namespace DomcheBGLTD.Controllers;

[Authorize]
[Route("[controller]/[action]/{id?}")]   // gives /ManageListings/Edit/7 on both verbs
public class ManageListingsController : Controller
{
    private readonly ApplicationDbContext _ctx;
    
    private const long FileLimit = 5 * 1024 * 1024;
    
    public ManageListingsController(ApplicationDbContext ctx) => _ctx = ctx;

    /* ----------  GET /ManageListings/My ---------- */
    public async Task<IActionResult> My()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var myAds = await _ctx.Listings
                               .Where(l => l.OwnerId == userId && !l.IsDeleted)
                               .Include(l => l.Images)
                               .ToListAsync();

        var cards = myAds.Select(ListingCardVmMapper.From);
        return View(cards);                     // Views/ManageListings/My.cshtml
    }

    /* ----------  GET: Create form ---------- */
    [HttpGet]                            // or omit – GET is default
    public IActionResult Create()
    {
        return View(new CreateListingVm());
    }
    
    /* ----------  POST: handle form ---------- */
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateListingVm vm)
    {
        if (!ModelState.IsValid) return View(vm);

        var ownerId = User.GetUserId();
        var entity = vm.ToEntity(ownerId);

        const long limit = 5 * 1024 * 1024;
        foreach (var file in vm.Images ?? Enumerable.Empty<IFormFile>())
        {
            if (file.Length is 0 or > limit)
            {
                ModelState.AddModelError("", $"Image {file.FileName} is empty or too large.");
                return View(vm);
            }

            await using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            entity.Images.Add(new ListingImage
            {
                Data = ms.ToArray(),
                ContentType = file.ContentType,
                Order = entity.Images.Count
            });
        }

        _ctx.Add(entity);
        await _ctx.SaveChangesAsync();
        return RedirectToAction(nameof(My));
    }

    /* ----------  GET: Edit  ---------- */
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var listing = await _ctx.Listings
                                .Include(l => l.Images)
                                .FirstOrDefaultAsync(l => l.Id == id && l.OwnerId == userId);

        if (listing == null) return NotFound();

        var vm = new CreateListingVm
        {
            Title = listing.Title,
            Description = listing.Description,
            ListingType = listing.ListingType,
            PropertyType = listing.PropertyType,
            Price = listing.Price,
            AreaM2 = listing.AreaM2,
            Province = listing.Province,
            City = listing.City
        };

        ViewBag.ListingId = id;
        ViewBag.ExistingIds = listing.Images.OrderBy(i => i.Order)
                                            .Select(i => i.Id).ToArray();
        return View(vm);                             // Views/ManageListings/Edit.cshtml
    }

    /* ----------  POST: Edit  ---------- */
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateListingVm vm)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var listing = await _ctx.Listings
                                .Include(l => l.Images)
                                .FirstOrDefaultAsync(l => l.Id == id && l.OwnerId == userId);

        if (listing == null) return NotFound();
        if (!ModelState.IsValid)
        {
            ViewBag.ListingId = id;
            ViewBag.ExistingIds = listing.Images.Select(i => i.Id).ToArray();
            return View(vm);
        }

        listing.UpdateFrom(vm);
        listing.UpdatedUtc = DateTime.UtcNow;

        bool wipe = Request.Form["removeExisting"] == "true";
        if (wipe)
        {
            _ctx.ListingImages.RemoveRange(listing.Images);
            listing.Images.Clear();
        }

        foreach (var file in vm.Images ?? Enumerable.Empty<IFormFile>())
        {
            if (file.Length == 0 || file.Length > FileLimit) continue;

            await using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            listing.Images.Add(new ListingImage
            {
                Data = ms.ToArray(),
                ContentType = file.ContentType,
                Order = listing.Images.Count
            });
        }

        await _ctx.SaveChangesAsync();
        return RedirectToAction(nameof(My));
    }

    /* ----------  GET: Delete confirmation ---------- */
    public async Task<IActionResult> Delete(int id)
    {
        var ad = await _ctx.Listings
                           .FirstOrDefaultAsync(l => l.Id == id && l.OwnerId == User.GetUserId());
        if (ad == null) return NotFound();
        return View(ad);                       // ask “Are you sure?”
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var ad = await _ctx.Listings.FindAsync(id);
        if (ad == null || ad.OwnerId != User.GetUserId())
            return NotFound();

        ad.IsDeleted = true;
        await _ctx.SaveChangesAsync();
        return RedirectToAction(nameof(My));
    }
}
