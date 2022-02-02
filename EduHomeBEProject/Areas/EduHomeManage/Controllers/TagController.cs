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
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Tags.Count() / 3);
            List<Tag> model = _context.Tags.Include(t => t.CourseTags).Skip((page-1)*3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag)
        {
            if (!ModelState.IsValid)  return View(); 
            Tag sname = _context.Tags.FirstOrDefault(t => t.Name.ToLower() == tag.Name.ToLower());
            if (sname != null)
            {
                ModelState.AddModelError("", "This name existed,try different");
                return View();
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            return View(tag);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Tag tag)
        {
            
            if (!ModelState.IsValid) return View();

            Tag exTag = _context.Tags.FirstOrDefault(t => t.Id == tag.Id);
            Tag checkName = _context.Tags.FirstOrDefault(c => c.Name == exTag.Name);
            if (checkName != null)
            {
                ModelState.AddModelError("Name", "Name is existed");
                return View(exTag);
            }
            if (exTag == null)
            {
                return NotFound();
            }
            Tag samename = _context.Tags.FirstOrDefault(t => t.Name.ToLower().Trim() == tag.Name.ToLower().Trim());
            if (samename != null)
            {
                ModelState.AddModelError("", "This name existed,try different one");
                return View();
            }
            exTag.Name = tag.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Tag tag = _context.Tags.FirstOrDefault(t => t.Id == id);
            if (tag == null)
                return Json(new { status = 404 });
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
