using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using EduHomeBEProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Controllers
{
    public class EventController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public EventController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Events.Count() / 3);
            List<Event> model = _context.Events.Include(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker).Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string EvSearch)
        {
            ViewData["GetEventdetails"] = EvSearch;
            var evqury = from x in _context.Events select x;
            if (!String.IsNullOrEmpty(EvSearch))
            {
                evqury = evqury.Where(x => x.Name.Contains(EvSearch));
            }
            return View(await evqury.AsNoTracking().ToListAsync());
        }
        public IActionResult Search(string search)
        {
            List<Event> events = _context.Events.Where(c => c.Name.ToLower().Trim().Contains(search.ToLower().Trim())).ToList();
            return PartialView("_EventPartialView", events);
        }
        public IActionResult EventRightSide(/*int page = 1*/)
        {
            //ViewBag.CurrentPage = page;
            //ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);
            //List<Blog> model = _context.Blogs.Include(b => b.Comments).ThenInclude(b => b.AppUser).Skip((page - 1) * 3).Take(3).ToList();
            List<Event> events = _context.Events.Include(e => e.EventSpeakers).ThenInclude(es => es.Speaker).Include(e => e.EComments).ThenInclude(e => e.AppUser).ToList();
            return View(events);
        }
        public IActionResult Details(int id)
        {
            Event eventt = _context.Events.Include(e=>e.EventSpeakers).ThenInclude(es=>es.Speaker).Include(e=>e.EComments).ThenInclude(e=>e.AppUser).FirstOrDefault(c => c.Id == id);
            if (eventt == null)
            {
                return NotFound();
            }
            return View(eventt);
        }

        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(EComment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Details", "Event", new { id = comment.EventId });
            if (!_context.Events.Any(c => c.Id == comment.EventId))
                return NotFound();
            EComment ecmnt = new EComment
            {
                Text = comment.Text,
                Subject = comment.Subject,
                EventId = comment.EventId,
                WriteTime = DateTime.Now,
                AppUserId = user.Id
            };
            _context.EComments.Add(ecmnt);
            _context.SaveChanges();
            return RedirectToAction("Details", "Event", new { id = comment.EventId });
        }

        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!ModelState.IsValid) return RedirectToAction("Details", "Event");
            if (!_context.EComments.Any(c => c.Id == id && c.AppUserId == user.Id))
                return NotFound();
            EComment comment = _context.EComments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);

            _context.EComments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Event", new { id = comment.EventId });
        }
    }
}
