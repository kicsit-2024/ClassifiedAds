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
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,LogoUrl,Id,Logo")] Category model)
        {
            model.MakeSafe();

            //if (model.Logo == null || model.Logo.Length == 0)
            //{
            //    ModelState.AddModelError("Logo", "Invalid file or file missing");
            //}

            //var ext = Path.GetExtension(model.Logo.FileName).ToLower().TrimStart('.');
            //if (!Globals.SafeExtensionForUpload.Contains(ext))
            //{
            //    ModelState.AddModelError("Logo", $"{ext} extsion not allowed. Only {string.Join(", ", Globals.SafeExtensionForUpload)} extensions are allowed");
            //    return View(model);
            //}

            //var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "categories");

            //Directory.CreateDirectory(uploadsFolder);

            //var fileName = Guid.NewGuid().ToString() + "." + ext;
            //var filePath = Path.Combine(uploadsFolder, fileName);

            //using (FileStream fs = new(filePath, FileMode.Create))
            //{
            //    model.Logo.CopyTo(fs);
            //}

            if (!FileUploadHelper.TryUpload(model.Logo, "categories", out string result))
            {
                ModelState.AddModelError("Logo", result);
                return View(model);
            }
            else
            {
                model.LogoUrl = result;
            }

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                if (System.IO.File.Exists(model.LogoUrl))
                {
                    System.IO.File.Delete(model.LogoUrl);
                }
            }
            return View(model);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Description,Logo,Id")] Category model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (FileUploadHelper.TryUpload(model.Logo, "categories", out string result))
            {
                model.LogoUrl = result;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    if (string.IsNullOrEmpty(model.LogoUrl))
                    {
                        _context.Entry(model).Property(m => m.LogoUrl).IsModified = false;
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(model.Id))
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
            return View(model);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
