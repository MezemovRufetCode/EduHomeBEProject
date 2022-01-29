using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Category> model = _context.Categories.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
                //return Content("Max length can be 20");
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category exCategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (exCategory == null)
            {
                return NotFound();
            }
            Category sname = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == category.Name.ToLower());
            if (sname != null)
            {
                ModelState.AddModelError("", "This name existed,try different");
                return View();
            }
            exCategory.Name = category.Name;
            //_context.Categories.Remove(exCategory);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                return Json(new { status = 404 });
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
