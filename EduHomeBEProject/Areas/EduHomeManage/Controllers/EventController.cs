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
            List<Event> model = _context.Events.Include(e => e.EventSpeakers).Include(e=>e.EComments).Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Event eventt)
        {
            ViewBag.Speakers = _context.Speakers.ToList();
            if (!ModelState.IsValid) return View();

            eventt.EventSpeakers = new List<EventSpeaker>();
            foreach (int id in eventt.SpeakerIds)
            {
                EventSpeaker eSpeaker = new EventSpeaker
                {
                    Event = eventt,
                    SpeakerId = id
                };
                eventt.EventSpeakers.Add(eSpeaker);
            }

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
            if (!ModelState.IsValid)
                return View();
            ViewBag.Speakers = _context.Speakers.ToList();
            Event eventt = _context.Events.Include(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker).FirstOrDefault(e => e.Id == id);
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
            ViewBag.Speakers = _context.Speakers.ToList();
            Event exEvent = _context.Events.Include(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker).FirstOrDefault(e => e.Id == eventt.Id);
            Event checkName = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker).FirstOrDefault(e => e.Name == exEvent.Name);
            if (checkName != null)
            {
                ModelState.AddModelError("Name", "Name is existed,try different one");
                return View(exEvent);
            }
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

            List<EventSpeaker> removableSpeaker = exEvent.EventSpeakers.Where(fc => !eventt.SpeakerIds.Contains(fc.Id)).ToList();
            exEvent.EventSpeakers.RemoveAll(fc => removableSpeaker.Any(rc => fc.Id == rc.Id));
            foreach (var speakerId in eventt.SpeakerIds)
            {
                EventSpeaker eventSpeaker = exEvent.EventSpeakers.FirstOrDefault(fc => fc.SpeakerId == speakerId);
                if (eventSpeaker == null)
                {
                    EventSpeaker espeaker = new EventSpeaker
                    {
                        SpeakerId = speakerId,
                        EventId = exEvent.Id
                    };
                    exEvent.EventSpeakers.Add(espeaker);
                }
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


        public IActionResult Comments(int EventId)
        {
            if (!_context.EComments.Any(e => e.EventId == EventId))
                return RedirectToAction("Index", "Event");
            List<EComment> ecomments = _context.EComments.Include(c => c.AppUser).Where(e => e.EventId == EventId).ToList();
            return View(ecomments);
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
