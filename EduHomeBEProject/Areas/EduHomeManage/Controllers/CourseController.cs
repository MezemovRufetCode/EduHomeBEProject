using EduHomeBEProject.DAL;
using EduHomeBEProject.Extensions;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseController(AppDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Course> model = _context.Courses.ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            ViewBag.Tags = _context.Tags.ToList();
            if (!ModelState.IsValid) return View();

            course.CourseTags = new List<CourseTag>();
            foreach (int id in course.TagIds)
            {
                CourseTag cTag = new CourseTag
                {
                    Course = course,
                    TagId = id
                };
                course.CourseTags.Add(cTag);
            }



            if (course.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You can input only image");
                return View();
            }
            if (!course.ImageFile.CheckSize(2))
            {
                ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                return View();
            }

            if (!course.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Please choose image file");
                return View();
            }
            course.Image = course.ImageFile.SaveImg(_env.WebRootPath, "assets/img/course");

            _context.Courses.Add(course);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
