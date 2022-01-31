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
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 3);
            List<Course> model = _context.Courses.Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();
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
            if (course.ImageFile != null)
            {
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

            }


            _context.Courses.Add(course);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            Course course = _context.Courses.Include(c => c.CourseTags).FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course course)
        {
            ViewBag.Tags = _context.Tags.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            Course exCourse = _context.Courses.Include(c => c.CourseTags).FirstOrDefault(c => c.Id == course.Id);
            if (!ModelState.IsValid) return View();
            if (exCourse == null) return NotFound();

            if (course.ImageFile != null)
            {
                if (!course.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please select image file only");
                    return View(exCourse);
                }
                if (!course.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFiles", "Image size max can be 2 mb");
                    return View(exCourse);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/course", exCourse.Image);
                exCourse.Image = course.ImageFile.SaveImg(_env.WebRootPath, "assets/img/course");
            }


            List<CourseTag> removableTag = exCourse.CourseTags.Where(fc => !course.TagIds.Contains(fc.Id)).ToList();
            exCourse.CourseTags.RemoveAll(fc => removableTag.Any(rc => fc.Id == rc.Id));
            foreach (var tagId in course.TagIds)
            {
                CourseTag courseTag = exCourse.CourseTags.FirstOrDefault(fc => fc.TagId == tagId);
                if (courseTag == null)
                {
                    CourseTag ctag = new CourseTag
                    {
                        TagId = tagId,
                        CourseId = exCourse.Id
                    };
                    exCourse.CourseTags.Add(ctag);
                }
            }

            //bunu yazanda umimiyyetle kursu silir
            //exCourse.Category = course.Category;
            exCourse.Name = course.Name;
            exCourse.Description = course.Description;
            exCourse.About = course.About;
            exCourse.Apply = course.Apply;
            exCourse.Certification = course.Certification;
            exCourse.Starts = course.Starts;
            exCourse.Duration = course.Duration;
            exCourse.ClassDuration = course.ClassDuration;
            exCourse.SkillLevel = course.SkillLevel;
            exCourse.Language = course.Language;
            exCourse.StudentCount = course.StudentCount;
            exCourse.Price = course.Price;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Course course = _context.Courses.FirstOrDefault(c => c.Id == id);
            if (course == null)
                return Json(new { status = 404 });
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }

        //public IActionResult Features(int CourseId)
        //{
        //    if (!_context.CourseFeatures.Any(f => f.CourseId != CourseId))
        //        return RedirectToAction("Index", "Course");
        //    List<CourseFeature> courseFeatures = _context.CourseFeatures.Where(f => f.CourseId == CourseId).ToList();
        //    return View();
        //}
    }
}
