using DomcheBGLTD.Data;
using DomcheBGLTD.Models.Entities;
using DomcheBGLTD.Models.Helpers;
using DomcheBGLTD.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DomcheBGLTD.Controllers
{
    public class ListingsController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public ListingsController(ApplicationDbContext ctx) => _ctx = ctx;

        /* ----------  Browse page (#3) ---------- */
        // GET: /Listings/Browse
        [HttpGet]
        public IActionResult Browse()
        {
            var vm = new BrowseFilterVm();
            PopulateFilterOptions(vm);
            // Optionally: don't show results until user submits
            vm.Results = Enumerable.Empty<ListingCardVm>();
            return View(vm);
        }


        // POST: /Listings/Browse
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Browse(BrowseFilterVm f)
        {
            PopulateFilterOptions(f);

            IQueryable<Listing> q = _ctx.Listings
                .Include(l => l.Images)
                .Include(l => l.Features)
                .Where(l => l.IsApproved && !l.IsDeleted);

            if (f.Listing is not null)
                q = q.Where(l => l.ListingType == f.Listing);

            if (f.SelectedPropertyTypes.Any())
                q = q.Where(x => f.SelectedPropertyTypes.Contains(x.PropertyTypeId));

            if (f.SelectedConstructionTypes.Any())
                q = q.Where(x => f.SelectedConstructionTypes.Contains(x.ConstructionTypeId));

            if (f.HasPictures)
                q = q.Where(l => l.Images.Any());

            if (f.MinAreaM2 is not null) q = q.Where(l => l.AreaM2 >= f.MinAreaM2);
            if (f.MaxAreaM2 is not null) q = q.Where(l => l.AreaM2 <= f.MaxAreaM2);
            if (f.Floor is not null) q = q.Where(l => l.Floor == f.Floor);
            if (f.FloorsTotal is not null) q = q.Where(l => l.FloorsTotal == f.FloorsTotal);
            if (f.SelectedFeatures.Any())
                q = q.Where(l => l.Features.Any(feat => f.SelectedFeatures.Contains(feat.Id)));

            f.Results = q
                .AsNoTracking()
                .OrderByDescending(l => l.CreatedUtc)
                .ToList()
                .Select(ListingCardVmMapper.From);

            return View(f);
        }

        private void PopulateFilterOptions(BrowseFilterVm f)
        {
            f.AvailableFeatures = _ctx.FeatureTypes
                .Select(ft => new SelectListItem
                {
                    Value = ft.Id.ToString(),
                    Text = ft.Name
                }).ToList();

            f.PropertyTypes = _ctx.PropertyTypes
                .Select(pt => new SelectListItem
                {
                    Value = pt.Id.ToString(),
                    Text = pt.Name
                }).ToList();

            f.ConstructionTypes = _ctx.ConstructionTypes
                .Select(ct => new SelectListItem
                {
                    Value = ct.Id.ToString(),
                    Text = ct.Name
                }).ToList();

            f.Cities = _ctx.Cities
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList();
        }
        /* ----------  Details page  ---------- */
        public IActionResult Details(int id)
        {
            var listing = _ctx.Listings
                              .Include(l => l.Images)
                              .Include(l => l.Owner)
                              .FirstOrDefault(l => l.Id == id && !l.IsDeleted && l.IsApproved);

            if (listing == null) return NotFound();
            return View(listing);                          // a view called Views/Listings/Details.cshtml
        }

        // ListingsController (public)
        [HttpGet("img/{id:int}")]
        public IActionResult Image(int id)
        {
            var img = _ctx.ListingImages.Find(id);
            if (img == null) return NotFound();
            return File(img.Data, img.ContentType);   // streams bytes to the browser
        }

    }
}
