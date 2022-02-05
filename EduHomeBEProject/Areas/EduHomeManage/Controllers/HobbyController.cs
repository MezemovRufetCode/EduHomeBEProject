using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class HobbyController : Controller
    {
        private readonly AppDbContext _context;
        public HobbyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Hobbies.Count() / 3);
            List<Hobby> model = _context.Hobbies.Include(h => h.TeacherHobbies).Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Hobby hobby)
        {
            if (!ModelState.IsValid) return View();
            Hobby sname = _context.Hobbies.FirstOrDefault(h => h.Name.ToLower().Trim() == hobby.Name.ToLower().Trim());
            if (sname != null)
            {
                ModelState.AddModelError("", "This name existed,try different");
                return View();
            }
            _context.Hobbies.Add(hobby);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Hobby hobby = _context.Hobbies.FirstOrDefault(h => h.Id == id);
            return View(hobby);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Hobby hobby)
        {

            if (!ModelState.IsValid) return View();

            Hobby exHobby = _context.Hobbies.FirstOrDefault(h => h.Id == hobby.Id);
            Hobby checkName = _context.Hobbies.FirstOrDefault(c => c.Name == exHobby.Name);
            if (checkName != null)
            {
                ModelState.AddModelError("Name", "Name is existed,try different one");
                return View(exHobby);
            }
            if (exHobby == null)
            {
                return NotFound();
            }
            Hobby samename = _context.Hobbies.FirstOrDefault(t => t.Name.ToLower().Trim() == hobby.Name.ToLower().Trim());
            if (samename != null)
            {
                ModelState.AddModelError("", "This name existed,try different one");
                return View();
            }
            exHobby.Name = hobby.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Hobby hobby = _context.Hobbies.FirstOrDefault(h => h.Id == id);
            if (hobby == null)
                return Json(new { status = 404 });
            _context.Hobbies.Remove(hobby);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
