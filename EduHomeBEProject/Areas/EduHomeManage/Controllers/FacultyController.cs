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
    public class FacultyController : Controller
    {
        private readonly AppDbContext _context;
        public FacultyController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Faculties.Count() / 3);
            List<Faculty> model = _context.Faculties.Include(f=>f.TeacherFaculties).Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Faculty faculty)
        {
            if (!ModelState.IsValid) return View();
            Faculty sname = _context.Faculties.FirstOrDefault(f => f.Name.ToLower().Trim() == faculty.Name.ToLower().Trim());
            if (sname != null)
            {
                ModelState.AddModelError("", "This name existed,try different");
                return View();
            }
            _context.Faculties.Add(faculty);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Faculty faculty = _context.Faculties.FirstOrDefault(f => f.Id == id);
            return View(faculty);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Faculty faculty)
        {

            if (!ModelState.IsValid) return View();

            Faculty exFaculty = _context.Faculties.FirstOrDefault(t => t.Id == faculty.Id);
            Faculty checkName = _context.Faculties.FirstOrDefault(c => c.Name == exFaculty.Name);
            if (checkName != null)
            {
                ModelState.AddModelError("Name", "Name is existed,try different one");
                return View(exFaculty);
            }
            if (exFaculty == null)
            {
                return NotFound();
            }
            Faculty samename = _context.Faculties.FirstOrDefault(t => t.Name.ToLower().Trim() == faculty.Name.ToLower().Trim());
            if (samename != null)
            {
                ModelState.AddModelError("", "This name existed,try different one");
                return View();
            }
            exFaculty.Name = faculty.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Faculty faculty = _context.Faculties.FirstOrDefault(t => t.Id == id);
            if (faculty == null)
                return Json(new { status = 404 });
            _context.Faculties.Remove(faculty);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
