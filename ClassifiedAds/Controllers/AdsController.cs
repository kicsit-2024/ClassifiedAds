using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClassifiedAds.Data;
using ClassifiedAds.Models;
using ClassifiedAds.Helpers;

namespace ClassifiedAds.Controllers
{
    public class AdsController : Controller
    {
        private readonly AppDbContext _context;

        public AdsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Ads
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Ads.Include(a => a.Category).Include(a => a.Group).Include(a => a.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Category)
                .Include(a => a.Group)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // GET: Ads/Create
        public IActionResult Create(int categoryId)
        {
            
            var category = _context.Categories.Where(m => m.Id == categoryId).Include(m => m.Specs).ThenInclude(m => m.Group).FirstOrDefault();
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.category = category;
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ad model)
        {
            if(model.Uploads?.Count > 0)
            {
                model.Images = [];
                byte rank = 1;
                foreach (var item in model.Uploads)
                {
                    if (!FileUploadHelper.TryUpload(item, "ads", out string result))
                    {
                        ModelState.AddModelError("Uploads", result);
                        return View(model);
                    }
                    else
                    {
                        model.Images.Add(new AdImage { ImageUrl = result, Rank = rank++ });
                    }
                }
            }

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var errors = ModelState.Values.Where(m => m.Errors.Any()).ToList();
                var x = 10;
            }
           
            return View(model);
        }

        // GET: Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", ad.CategoryId);
            ViewData["GroupId"] = new SelectList(_context.AdvertisementGroups, "Id", "Id", ad.GroupId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ad.UserId);
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Price,Title,Description,AdNature,GoogleMap,CategoryId,UserId,GroupId,Id,Token")] Ad ad)
        {
            if (id != ad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", ad.CategoryId);
            ViewData["GroupId"] = new SelectList(_context.AdvertisementGroups, "Id", "Id", ad.GroupId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", ad.UserId);
            return View(ad);
        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Category)
                .Include(a => a.Group)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ad = await _context.Ads.FindAsync(id);
            if (ad != null)
            {
                _context.Ads.Remove(ad);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(int id)
        {
            return _context.Ads.Any(e => e.Id == id);
        }
    }
}
