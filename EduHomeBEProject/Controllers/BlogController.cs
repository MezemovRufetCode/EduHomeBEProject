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

    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        public BlogController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);
            List<Blog> model = _context.Blogs.Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
            //return Content(id.ToString());
        }
        public IActionResult BlogRightSide(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);
            List<Blog> model = _context.Blogs.Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }

    }
}
