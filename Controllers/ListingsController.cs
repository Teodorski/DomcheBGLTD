using DomcheBGLTD.Data;
using DomcheBGLTD.Models.Entities;
using DomcheBGLTD.Models.Helpers;
using DomcheBGLTD.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DomcheBGLTD.Controllers
{
    public class ListingsController : Controller
    {
        private readonly ApplicationDbContext _ctx;

        public ListingsController(ApplicationDbContext ctx) => _ctx = ctx;

        /* ----------  Browse page (#3) ---------- */
        public IActionResult Browse(BrowseFilterVm f)
        {
            IQueryable<Listing> q = _ctx.Listings
                .Include(l => l.Images)
                .Where(l => l.IsApproved && !l.IsDeleted);

            if (f.Listing is not null)
                q = q.Where(l => l.ListingType == f.Listing);

            //if (f.Property.Any())
            //    q = q.Where(l => f.Property.Contains(l.PropertyType));
            //if (!string.IsNullOrWhiteSpace(f.City)) q = q.Where(l => l.City == f.City);
            //if (f.MinPrice is not null) q = q.Where(l => l.Price >= f.MinPrice);
            //if (f.MaxPrice is not null) q = q.Where(l => l.Price <= f.MaxPrice);

            f.Results = q
                .AsNoTracking()
                .OrderByDescending(l => l.CreatedUtc)
                .ToList()
                .Select(ListingCardVmMapper.From);

            return View(f);
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

        ///* ----------  Create listing (#2) ---------- */
        [Authorize]
        public IActionResult Create() => View(new CreateListingVm());

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateListingVm vm)
        {
            //if (!ModelState.IsValid) return View(vm);

            //var entity = vm.ToEntity(User.GetUserId());   // extension method for current user id
            //_ctx.Add(entity);
            //await _ctx.SaveChangesAsync();
            return RedirectToAction("My", "ManageListings");
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
