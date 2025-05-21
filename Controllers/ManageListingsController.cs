using DomcheBGLTD.Data;
using DomcheBGLTD.Models;
using DomcheBGLTD.Models.Helpers;
using DomcheBGLTD.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DomcheBGLTD.Models.Entities;
using DomcheBGLTD.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

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
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var vm = new CreateListingVm();
        await PopulateVmLookups(vm);
        return View(vm);
    }

    /* ----------  POST: handle form ---------- */
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateListingVm vm)
    {
        // if invalid, re-populate lookup lists and return
        if (!ModelState.IsValid)
        {
            await PopulateVmLookups(vm);
            return View(vm);
        }

        // 1) get current user
        var ownerId = User.GetUserId();

        // 2) load the selected FeatureType entities
        var selectedFeatures = await _ctx.FeatureTypes
            .Where(ft => vm.Features.Contains(ft.Id))
            .ToListAsync();

        // 3) map VM → Listing (including Features)
        var listing = vm.ToEntity(ownerId, selectedFeatures);

        // 4) process uploaded images
        foreach (var file in vm.Images ?? Enumerable.Empty<IFormFile>())
        {
            if (file.Length is 0 or > FileLimit)
            {
                ModelState.AddModelError("", $"Image \"{file.FileName}\" is empty or exceeds 5 MB.");
                await PopulateVmLookups(vm);
                return View(vm);
            }

            await using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            listing.Images.Add(new ListingImage
            {
                Data = ms.ToArray(),
                ContentType = file.ContentType,
                Order = listing.Images.Count
            });
        }

        await _ctx.Listings.AddAsync(listing);
        await _ctx.SaveChangesAsync();
        return RedirectToAction(nameof(My));  // or wherever you want
    }

    /// <summary>
    /// Populates all the <see cref="SelectListItem"/> collections on the VM.
    /// </summary>
    private async Task PopulateVmLookups(CreateListingVm vm)
    {
        vm.PropertyTypes = await _ctx.PropertyTypes
            .OrderBy(pt => pt.Name)
            .Select(pt => new SelectListItem(pt.Name, pt.Id.ToString()))
            .ToListAsync();

        vm.Cities = await _ctx.Cities
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
            .ToListAsync();

        vm.Currencies = await _ctx.Currencies
            .OrderBy(cu => cu.Code)
            .Select(cu => new SelectListItem(cu.Code, cu.Id.ToString()))
            .ToListAsync();

        vm.ConstructionTypes = await _ctx.ConstructionTypes
            .OrderBy(ct => ct.Name)
            .Select(ct => new SelectListItem(ct.Name, ct.Id.ToString()))
            .ToListAsync();

        vm.AvailableFeatures = await _ctx.FeatureTypes
            .OrderBy(ft => ft.Name)
            .Select(ft => new SelectListItem(ft.Name, ft.Id.ToString()))
            .ToListAsync();
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
