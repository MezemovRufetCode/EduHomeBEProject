using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using EduHomeBEProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        public EventController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Courses.Count() / 3);
            List<Event> model = _context.Events.Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Event eventt = _context.Events.FirstOrDefault(c => c.Id == id);
            if (eventt == null)
            {
                return NotFound();
            }
            return View(eventt);
            //return Content(id.ToString());
        }
    }
}
