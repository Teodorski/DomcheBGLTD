using DomcheBGLTD.Data;
using DomcheBGLTD.Models;
using DomcheBGLTD.Models.Helpers;
using DomcheBGLTD.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using DomcheBGLTD.Models.DTOs;
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
            .Where(ft => vm.SelectedFeatures.Contains(ft.Id))
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
        return RedirectToAction(nameof(My));
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

    /* ----------  GET: Edit form ---------- */
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var listing = await _ctx.Listings
            .Include(l => l.Features)
            .Include(l => l.Images)
            .FirstOrDefaultAsync(l => l.Id == id && l.OwnerId == User.GetUserId());

        if (listing == null) return NotFound();

        var vm = new CreateListingVm
        {
            Title = listing.Title,
            Description = listing.Description,
            ListingType = listing.ListingType,
            PropertyTypeId = listing.PropertyTypeId,
            Price = listing.Price,
            CurrencyId = listing.CurrencyId,
            AreaM2 = listing.AreaM2,
            YearBuilt = listing.YearBuilt,
            CityId = listing.CityId,
            Address1 = listing.Address1,
            Address2 = listing.Address2,
            Floor = listing.Floor,
            FloorsTotal = listing.FloorsTotal,
            ConstructionTypeId = listing.ConstructionTypeId,
            SelectedFeatures = listing.Features.Select(f => f.Id).ToList(),
            Phone = listing.Phone,
            Email = listing.Email,
            AdditionalInformation = listing.ExtraInfo,
            ExistingImages = listing.Images.Select(i => new ListingImageDto()
            {
                ContentType = i.ContentType,
                Data = i.Data,
                Id = i.Id,
            }).ToList()
        };

        await PopulateVmLookups(vm);
        ViewData["Title"] = "Edit Listing";
        return View(vm);
    }

    /* ----------  POST: handle form ---------- */
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CreateListingVm vm)
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
            .Where(ft => vm.SelectedFeatures.Contains(ft.Id))
            .ToListAsync();

        // 3) map VM → Listing (including Features)
        var listingEntity = await _ctx.Listings
            .Include(l => l.Features)
            .Include(l => l.Images)
            .FirstOrDefaultAsync(l => l.Id == id && l.OwnerId == ownerId);

        if (listingEntity == null)
        {
            return NotFound(vm);
        }
        listingEntity.Title = vm.Title;
        listingEntity.Description = vm.Description;
        listingEntity.Price = vm.Price;

        listingEntity.Features.Clear();
        foreach (var feature in selectedFeatures)
        {
            listingEntity.Features.Add(feature);
        }

        if (vm.RemoveImageIds?.Any() == true)
        {
            vm.RemoveImageIds.ForEach(id =>
            {
                var image = listingEntity.Images.FirstOrDefault(i => i.Id == id);
                if (image != null)
                {
                    listingEntity.Images.Remove(image);
                }
            });
        }

        listingEntity.Images.Clear();
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

            listingEntity.Images.Add(new ListingImage
            {
                Data = ms.ToArray(),
                ContentType = file.ContentType,
                Order = listingEntity.Images.Count
            });
        }

        _ctx.Listings.Update(listingEntity);
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
