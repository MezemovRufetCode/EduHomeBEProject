using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using EduHomeBEProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usermanager;

        public HomeController(AppDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _usermanager = userManager;
        }

        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Sliders = _context.Sliders.ToList(),
                Settings = _context.Settings.FirstOrDefault(),
                Categories=_context.Categories.ToList(),
                Courses =_context.Courses.Include(c=>c.CourseTags).ThenInclude(ct=>ct.Tag).ToList(),
                Events=_context.Events.ToList(),
                Blogs=_context.Blogs.Include(b => b.Comments).ThenInclude(b => b.AppUser).ToList(),
                NoticeBoards = _context.NoticeBoards.ToList()
            };
            return View(homeVM);
        }
        public IActionResult SearchEvResult(string search)
        {
            HomeVM homeSearchVM = new HomeVM()
            {
                Courses = search == null ? _context.Courses.ToList() : _context.Courses.Where(c => c.Name.ToLower().Trim().Contains(search.ToLower().Trim())).ToList(),
                Events = search == null ? _context.Events.ToList() : _context.Events.Where(c => c.Name.ToLower().Trim().Contains(search.ToLower().Trim())).ToList(),
                Blogs = search == null ? _context.Blogs.Include(b => b.Comments).ThenInclude(b => b.AppUser).ToList() : _context.Blogs.Include(b => b.Comments).ThenInclude(b => b.AppUser).Where(c => c.Title.ToLower().Contains(search.ToLower())).ToList(),
                Teachers = search == null ? _context.Teachers.ToList() : _context.Teachers.Where(c => c.Name.ToLower().Trim().Contains(search.ToLower().Trim())).ToList()

                //Courses = _context.Courses.Where(c => c.Name.ToLower().Trim().Contains(search.ToLower().Trim())).ToList(),
                //Events = _context.Events.Where(c => c.Name.ToLower().Trim().Contains(search.ToLower().Trim())).ToList(),
                //Blogs = _context.Blogs.Include(b => b.Comments).ThenInclude(b => b.AppUser).Where(c => c.Title.ToLower().Contains(search.ToLower())).ToList(),
                //Teachers=_context.Teachers.Where(c => c.Name.ToLower().Trim().Contains(search.ToLower().Trim())).ToList()
            };
            return View(homeSearchVM);
        }
        public IActionResult Chat()
        {
            List<AppUser> model = _usermanager.Users.ToList();
            return View(model);
        }
    }
}
