using EduHomeBEProject.DAL;
using EduHomeBEProject.Extensions;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "SuperAdmin,Admin")]
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
            List<Teacher> model = _context.Teachers.Include(t=>t.TeacherFaculties).Include(t=>t.TeacherHobbies).Skip((page - 1) * 3).Take(3).ToList();
            if (!ModelState.IsValid) return View();
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Teachers.Count() / 3);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Socials = _context.Socials.ToList();
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Faculties = _context.Faculties.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Teacher teacher)
        {
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Socials = _context.Socials.ToList();
            if (!ModelState.IsValid) return View();

            teacher.TeacherHobbies = new List<TeacherHobby>();
            foreach (int id in teacher.HobbyIds)
            {
                TeacherHobby tHobby = new TeacherHobby
                {
                    Teacher = teacher,
                    HobbyId = id
                };
                teacher.TeacherHobbies.Add(tHobby);
            }
            teacher.TeacherFaculties = new List<TeacherFaculty>();
            foreach (int id in teacher.FacultyIds)
            {
                TeacherFaculty tFaculty = new TeacherFaculty
                {
                    Teacher = teacher,
                    FacultyId = id
                };
                teacher.TeacherFaculties.Add(tFaculty);
            }
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
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Socials = _context.Socials.ToList();
            Teacher teacher = _context.Teachers.Include(t=>t.TeacherFaculties).Include(t=>t.TeacherHobbies).FirstOrDefault(t => t.Id == id);
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
            ViewBag.Faculties = _context.Faculties.ToList();
            ViewBag.Hobbies = _context.Hobbies.ToList();
            ViewBag.Socials = _context.Socials.ToList();
            Teacher exTeach = _context.Teachers.Include(t => t.TeacherFaculties).Include(t => t.TeacherHobbies).FirstOrDefault(t=> t.Id == teacher.Id);
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

            List<TeacherFaculty> removableFaculty = exTeach.TeacherFaculties.Where(tf => !teacher.FacultyIds.Contains(tf.Id)).ToList();
            exTeach.TeacherFaculties.RemoveAll(fc => removableFaculty.Any(rf => fc.Id == rf.Id));
            foreach (var facultyId in teacher.FacultyIds)
            {
                TeacherFaculty teacherFaculty = exTeach.TeacherFaculties.FirstOrDefault(fc => fc.FacultyId == facultyId);
                if (teacherFaculty == null)
                {
                    TeacherFaculty tfak = new TeacherFaculty
                    {
                        FacultyId = facultyId,
                        TeacherId = exTeach.Id
                    };
                    exTeach.TeacherFaculties.Add(tfak);
                }
            }
            List<TeacherHobby> removableHobby = exTeach.TeacherHobbies.Where(th => !teacher.HobbyIds.Contains(th.Id)).ToList();
            exTeach.TeacherHobbies.RemoveAll(fc => removableHobby.Any(rh => fc.Id == rh.Id));
            foreach (var hobbyId in teacher.HobbyIds)
            {
                TeacherHobby teacherHobby = exTeach.TeacherHobbies.FirstOrDefault(fc => fc.HobbyId == hobbyId);
                if (teacherHobby == null)
                {
                    TeacherHobby thob = new TeacherHobby
                    {
                        HobbyId = hobbyId,
                        TeacherId = exTeach.Id
                    };
                    exTeach.TeacherHobbies.Add(thob);
                }
            }

            exTeach.FacebookAccount = teacher.FacebookAccount;
            exTeach.VimeoAccount = teacher.VimeoAccount;
            exTeach.TwitterAccount = teacher.TwitterAccount;
            exTeach.Pinterest = teacher.Pinterest;
            exTeach.Feature1 = teacher.Feature1;
            exTeach.Feature2 = teacher.Feature2;
            exTeach.Feature3 = teacher.Feature3;
            exTeach.Feature4 = teacher.Feature4;
            exTeach.Feature5 = teacher.Feature5;
            exTeach.Feature6 = teacher.Feature6;
            exTeach.FeatureVal1 = teacher.FeatureVal1;
            exTeach.FeatureVal2 = teacher.FeatureVal2;
            exTeach.FeatureVal3 = teacher.FeatureVal3;
            exTeach.FeatureVal4 = teacher.FeatureVal4;
            exTeach.FeatureVal5 = teacher.FeatureVal5;
            exTeach.FeatureVal6 = teacher.FeatureVal6;

            exTeach.Name = teacher.Name;
            exTeach.About = teacher.About;
            exTeach.Position = teacher.Position;
            exTeach.Email = teacher.Email;
            exTeach.Phone = teacher.Phone;
            exTeach.Degree = teacher.Degree;
            exTeach.Experience = teacher.Experience;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            Teacher teacher = _context.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher == null)
                return Json(new { status = 404 });
            _context.Teachers.Remove(teacher);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
