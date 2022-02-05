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
    public class SpeakerController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SpeakerController(AppDbContext context,IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Speakers.Count() / 3);
            List<Speaker> model = _context.Speakers.Include(t => t.EventSpeakers).Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Speaker speaker)
        {
            if (!ModelState.IsValid) return View();

            if (speaker.ImageFile != null)
            {
                if (!speaker.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                    return View();
                }

                if (!speaker.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Please choose image file");
                    return View();
                }
            }
            

            //bunu sonra fix eleyecem hazida img folerdinde ayrica speakers yoxdu,eventin icindedi
            speaker.Image = speaker.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");


            _context.Speakers.Add(speaker);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            Speaker speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            return View(speaker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Speaker speaker)
        {
            Speaker exSpeaker = _context.Speakers.FirstOrDefault(s => s.Id == speaker.Id);
            if (exSpeaker == null)
            {
                return NotFound();
            }
            if (!ModelState.IsValid) return View();
            if (speaker.ImageFile != null)
            {
                if (!speaker.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please select image file only");
                    return View();
                }
                if (!speaker.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFiles", "Image size max can be 2 mb");
                    return View();
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/event", exSpeaker.Image);
                exSpeaker.Image = speaker.ImageFile.SaveImg(_env.WebRootPath, "assets/img/event");
            }
            exSpeaker.Name = speaker.Name;
            exSpeaker.Position = speaker.Position;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Speaker speaker = _context.Speakers.FirstOrDefault(s => s.Id == id);
            if (speaker == null)
                return Json(new { status = 404 });
            _context.Speakers.Remove(speaker);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
