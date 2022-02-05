using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Categories.Count() / 3);
            List<AdrContact> model = _context.AdrContacts.Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdrContact adrContact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.AdrContacts.Add(adrContact);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            AdrContact adrContact = _context.AdrContacts.FirstOrDefault(a => a.Id == id);
            return View(adrContact);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AdrContact adrContact)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AdrContact exAdr = _context.AdrContacts.FirstOrDefault(a => a.Id == adrContact.Id);
            if (exAdr == null)
            {
                return NotFound();
            }
            AdrContact sStreet = _context.AdrContacts.FirstOrDefault(c => c.Street.ToLower() == adrContact.Street.ToLower());
            if (sStreet != null)
            {
                ModelState.AddModelError("", "This street existed,try different");
                return View();
            }
            exAdr.Street = adrContact.Street;
            exAdr.Country = adrContact.Country;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            AdrContact adrContact = _context.AdrContacts.FirstOrDefault(c => c.Id == id);
            if (adrContact == null)
                return Json(new { status = 404 });
            _context.AdrContacts.Remove(adrContact);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
