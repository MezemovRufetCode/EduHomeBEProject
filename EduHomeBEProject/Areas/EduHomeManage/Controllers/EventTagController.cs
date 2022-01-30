using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    public class EventTagController : Controller
    {
        private readonly AppDbContext _context;
        public EventTagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<ETag> model = _context.ETags.Include(t => t.EventTags).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ETag etag)
        {
            if (!ModelState.IsValid) { return View(); }
            ETag sname = _context.ETags.FirstOrDefault(t => t.Name.ToLower() == etag.Name.ToLower());
            if (sname != null)
            {
                ModelState.AddModelError("", "This name existed,try different");
                return View();
            }

            _context.ETags.Add(etag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ETag etag = _context.ETags.FirstOrDefault(t => t.Id == id);
            return View(etag);
        }

        [HttpPost]

        //Eyni adli iki tag yaratmaq olur editden sonra,onu duzeltmek
        public IActionResult Edit(ETag etag)
        {

            if (!ModelState.IsValid) return View();

            ETag exTag = _context.ETags.FirstOrDefault(t => t.Id == etag.Id);
            if (exTag == null)
            {
                return NotFound();
            }
            ETag samename = _context.ETags.FirstOrDefault(t => t.Name.ToLower().Trim() == etag.Name.ToLower().Trim());
            if (samename != null)
            {
                ModelState.AddModelError("", "This name existed,try different one");
                return View();
            }
            exTag.Name = etag.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            ETag etag = _context.ETags.FirstOrDefault(t => t.Id == id);
            if (etag == null)
                return Json(new { status = 404 });
            _context.ETags.Remove(etag);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
