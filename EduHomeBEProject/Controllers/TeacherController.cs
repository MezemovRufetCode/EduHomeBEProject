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
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;
        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage= Math.Ceiling((decimal)_context.Teachers.Count() / 4);
            List<Teacher> model = _context.Teachers.Include(t=>t.TeacherFaculties).ThenInclude(tf=>tf.Faculty).Include(t=>t.TeacherHobbies).ThenInclude(th=>th.Hobby).Skip((page - 1) * 4).Take(4).ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Teacher teacher = _context.Teachers.Include(t=>t.TeacherHobbies).ThenInclude(th => th.Hobby).Include(t=>t.TeacherFaculties).ThenInclude(tf => tf.Faculty).FirstOrDefault(t=> t.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
    }
}
