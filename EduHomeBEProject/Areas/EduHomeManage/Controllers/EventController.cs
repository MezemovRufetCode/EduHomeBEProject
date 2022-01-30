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
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EventController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Events.Count() / 3);
            List<Event> model = _context.Events.Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            //ViewBag.ETags = _context.ETags.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event eventt)
        {
            if (!ModelState.IsValid) return View();
            if (eventt.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You can input only image");
                return View();
            }
            if (!eventt.ImageFile.CheckSize(2))
            {
                ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                return View();
            }

            if (!eventt.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Please choose image file");
                return View();
            }
            eventt.Image = eventt.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");

            _context.Events.Add(eventt);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
         
            Event eventt = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventt == null)
            {
                return NotFound();
            }
            return View(eventt);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Event eventt)
        {
            Event exEvent = _context.Events.FirstOrDefault(e => e.Id == eventt.Id);
            if (!ModelState.IsValid) return View();

            if (eventt.ImageFile != null)
            {
                if (!eventt.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please select image file only");
                    return View(exEvent);
                }
                if (!eventt.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFiles", "Image size max can be 2 mb");
                    return View(exEvent);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", exEvent.Image);
                exEvent.Image = eventt.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");
            }

            exEvent.Name = eventt.Name;
            exEvent.Description = eventt.Description;
            exEvent.EventDay = eventt.EventDay;
            exEvent.StartTime = eventt.StartTime;
            exEvent.EndTime = eventt.EndTime;
            exEvent.Location = eventt.Location;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        public IActionResult Delete(int id)
        {
            Event eventt = _context.Events.FirstOrDefault(e => e.Id == id);
            if (eventt == null)
                return Json(new { status = 404 });
            _context.Events.Remove(eventt);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }

    }
}
