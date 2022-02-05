using EduHomeBEProject.DAL;
using EduHomeBEProject.Extensions;
using EduHomeBEProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SettingController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            Setting model = _context.Settings.FirstOrDefault();
            return View(model);
        }
        public IActionResult Edit(int id)
        {
            if (!ModelState.IsValid) return View();
            Setting setting = _context.Settings.FirstOrDefault(s => s.Id == id);
            return View(setting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Setting setting)
        {
            Setting exset = _context.Settings.FirstOrDefault(s => s.Id == setting.Id);
            if (!ModelState.IsValid) return View();
              if (setting.ImageFile != null)
            {
                if (!setting.ImageFile.IsImage())
                {
                    ModelState.AddModelError("ImageFile", "Please select image file only");
                    return View(exset);
                }
                if (!setting.ImageFile.CheckSize(2))
                {
                    ModelState.AddModelError("ImageFile", "Image size max can be 2 mb");
                    return View(exset);
                }
                Helpers.Helper.DeleteImg(_env.WebRootPath, "assets/img/logo", exset.Logo);
                exset.Logo = setting.ImageFile.SaveImg(_env.WebRootPath, "assets/img/logo");
            }

            exset.TopHeaderAnnounce = setting.TopHeaderAnnounce;
            exset.WelcomeEntry = setting.WelcomeEntry;
            exset.CourseSecTitle = setting.CourseSecTitle;
            exset.EventSecTitle = setting.EventSecTitle;
            exset.BlogSecTitle = setting.BlogSecTitle;
            exset.SubscribeSecTitle = setting.CourseSecTitle;
            exset.FacebookLink = setting.FacebookLink;
            exset.VimeoLink = setting.VimeoLink;
            exset.PinterestLink = setting.PinterestLink;
            exset.TwitterLink = setting.TwitterLink;
            exset.FooterDesc = setting.FooterDesc;
            exset.Adress = setting.Adress;
            exset.FContact = setting.FContact;
            exset.SContact = setting.SContact;
            exset.Email = setting.Email;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}

