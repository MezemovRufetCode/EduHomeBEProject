using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Controllers
{
    public class CourseController : Controller
    {
        private readonly AppDbContext _context;
        public CourseController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 3);
            List<Course> model = _context.Courses.Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).Skip((page - 1) * 2).Take(3).ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Course course = _context.Courses.Include(c => c.CourseTags).ThenInclude(ct => ct.Tag).FirstOrDefault(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
    }
}
