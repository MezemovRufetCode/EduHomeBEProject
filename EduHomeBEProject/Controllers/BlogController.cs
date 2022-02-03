using EduHomeBEProject.DAL;
using EduHomeBEProject.Models;
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

    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public BlogController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);
            List<Blog> model = _context.Blogs.Include(b=>b.Comments).ThenInclude(b=>b.AppUser).Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Details(int id)
        {
            Blog blog = _context.Blogs.Include(b=>b.Comments).ThenInclude(b=>b.AppUser).FirstOrDefault(b => b.Id == id);
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

        [Authorize]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddComment(bComment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
            if (!_context.Blogs.Any(b => b.Id == comment.BlogId))
                return NotFound();
            bComment bcmnt = new bComment
            {
                Text = comment.Text,
                Subject = comment.Subject,
                BlogId = comment.BlogId,
                WriteTime = DateTime.Now,
                AppUserId = user.Id
            };
            _context.bComments.Add(bcmnt);
            _context.SaveChanges();
            return RedirectToAction("Details", "Blog", new { id = comment.BlogId });
        }
        [Authorize]
        public async Task<IActionResult> DeleteComment(int id)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (!ModelState.IsValid) return RedirectToAction("Details", "Blog");
            if (!_context.bComments.Any(c => c.Id == id && c.AppUserId == user.Id))
                return NotFound();
            bComment bcomment = _context.bComments.FirstOrDefault(c => c.Id == id && c.AppUserId == user.Id);

            _context.bComments.Remove(bcomment);
            _context.SaveChanges();
            return RedirectToAction("Details", "Blog", new { id = bcomment.BlogId });
        }

    }
}
