using EduHomeBEProject.DAL;
using EduHomeBEProject.Extensions;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public TeacherController(AppDbContext context,IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            List<Teacher> model = _context.Teachers.Skip((page - 1) * 3).Take(3).ToList();
            if (!ModelState.IsValid) return View();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Teachers.Count() / 3);
            return View(model);
        }
        public IActionResult Create()
        {
            //ViewBag.Tags = _context.Tags.ToList();
            //ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            ViewBag.Socials = _context.Socials.ToList();
            if (!ModelState.IsValid) return View();

    
            if (teacher.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You can input only image");
                return View();
            }
            if (teacher.ImageFile != null)
            {
                if (!teacher.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                    return View();
                }

                if (!teacher.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Please choose image file");
                    return View();
                }
                teacher.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "assets/img/teacher");

            }


            _context.Teachers.Add(teacher);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Teacher teacher)
        {
            Teacher exTeach = _context.Teachers.FirstOrDefault(t=> t.Id == teacher.Id);
            if (!ModelState.IsValid) return View(exTeach);

            if (teacher.ImageFile != null)
            {
                if (!teacher.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please select image file only");
                    return View(exTeach);
                }
                if (!teacher.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFiles", "Image size max can be 2 mb");
                    return View(exTeach);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/teacher", exTeach.Image);
                exTeach.Image = teacher.ImageFile.SaveImg(_env.WebRootPath, "assets/img/teacher");
            }
            //if (teacher.Name == null)
            //{
            //    ModelState.AddModelError("Name", "Include teacher name");
            //    return View(exTeach);
            //}
            //if (teacher.About == null)
            //{
            //    ModelState.AddModelError("About", "Include about teacher");
            //    return View(exTeach);
            //}
            //if (teacher.Email == null)
            //{
            //    ModelState.AddModelError("Email", "Include email");
            //    return View(exTeach);
            //}
            //if (teacher.Phone == null)
            //{
            //    ModelState.AddModelError("Phone", "Include phone number");
            //    return View(exTeach);
            //}
            exTeach.Name = teacher.Name;
            exTeach.About = teacher.About;
            exTeach.Position = teacher.Position;
            exTeach.Email = teacher.Email;
            exTeach.Phone = teacher.Phone;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
