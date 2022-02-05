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
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.CurrentPage = page;
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 3);
            List<Blog> model = _context.Blogs.Include(b => b.Comments).Skip((page - 1) * 3).Take(3).ToList();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            if (!ModelState.IsValid) return View();
            if (blog.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "You can input only image");
                return View();
            }
            if (!blog.ImageFile.CheckSize(2))
            {
                ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                return View();
            }

            if (!blog.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Please choose image file");
                return View();
            }
            blog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "assets/img/blog");

            Blog blg = new Blog
            {
                Title = blog.Title,
                Desc = blog.Desc,
                PublishDate = DateTime.Now,
                Image = blog.Image,
                WrittenBy = User.Identity.Name
            };

            _context.Blogs.Add(blg);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {

            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog blog)
        {
            Blog exBlog = _context.Blogs.FirstOrDefault(b => b.Id == blog.Id);
            if (!ModelState.IsValid) return View();

            if (blog.ImageFile != null)
            {
                if (!blog.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file only");
                    return View(exBlog);
                }
                if (!blog.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                    return View(exBlog);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/blog", exBlog.Image);
                exBlog.Image = blog.ImageFile.SaveImg(_env.WebRootPath, "assets/img/blog");
            }

            exBlog.Title = blog.Title;
            exBlog.Desc = blog.Desc;
            exBlog.PublishDate = blog.PublishDate;
            exBlog.WrittenBy = blog.WrittenBy;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Comments(int BlogId)
        {
            if (!_context.bComments.Any(c => c.BlogId == BlogId))
                return RedirectToAction("Index", "Blog");
            List<bComment> comments = _context.bComments.Include(c => c.AppUser).Where(b => b.BlogId == BlogId).ToList();
            return View(comments);
        }
        public IActionResult Delete(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            if (blog == null)
                return Json(new { status = 404 });
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
