using ClassifiedAds.Data;
using ClassifiedAds.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedAds.Controllers
{
    public class LovsController(AppDbContext db) : Controller
    {
        private readonly AppDbContext _db = db;

        public IActionResult Index()
        {
            // select Id,Name, ShortCode from Lovs
            var data = _db.Lovs.ToList();
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(/*[Bind("Id, Name, ShortCode")]*/Lov model)
        {
            //if (!char.IsUpper(model.Title[0]))
            //{
            //    ModelState.AddModelError("Title", "Must start with uppercase");
            //}
            if (ModelState.IsValid)
            {
                _db.Add(model);
                //_db.Lovs.Add(model);
                //_db.Set<Lov>().Add(model);
                _db.SaveChanges();

                //return RedirectToAction("Index", "Home");
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = _db.Lovs.Find(id);
            if (model == null) return NotFound();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Lov model)
        {
            if (ModelState.IsValid)
            {
                _db.Update(model);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _db.Lovs.Find(id);
            //model.Title = "new title";
            if (model == null) return NotFound();
            return View(model);
        }

        [ActionName("Delete")]
        [HttpPost]
        public IActionResult DeleteConfirm(int id)
        {
            var model = _db.Lovs.Find(id);
            if (model == null) return NotFound();
            _db.Remove(model);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var model = _db.Lovs.Find(id);
            if (model == null) return NotFound();
            return View(model);
        }
    }
}
