using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    [Authorize(Roles ="SuperAdmin,Admin")]
    public class NoticeBoardController : Controller
    {
        private readonly AppDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInResult;
        public NoticeBoardController(AppDbContext context,RoleManager<IdentityRole> roleManager,SignInManager<AppUser> signInResult)
        {
            _context = context;
            _roleManager = roleManager;
            _signInResult = signInResult;
        }

        public IActionResult Index(int page=1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.NoticeBoards.Count() / 4);
            List<NoticeBoard> nboards = _context.NoticeBoards.Skip((page-1)*4).Take(4).ToList();
            return View(nboards);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoticeBoard NotBoard)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}
            NoticeBoard nboard = new NoticeBoard
            {
                Answer = NotBoard.Answer,
                CreateTime = DateTime.Now,
            };
            _context.NoticeBoards.Add(nboard);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            NoticeBoard ntboard = _context.NoticeBoards.FirstOrDefault(n => n.Id == id);
            if (ntboard == null)
            {
                return NotFound();
            }
            return View(ntboard);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(NoticeBoard board)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            NoticeBoard exnotboard= _context.NoticeBoards.FirstOrDefault(n => n.Id == board.Id);
            if (exnotboard == null)
            {
                return NotFound();
            }
            exnotboard.Answer = board.Answer;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            NoticeBoard nboard = _context.NoticeBoards.FirstOrDefault(t => t.Id == id);
            if (nboard == null)
            {
                return Json(new { status = 400 });
            }
            _context.NoticeBoards.Remove(nboard);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
