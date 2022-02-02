using EduHomeBEProject.Models;
using EduHomeBEProject.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Areas.EduHomeManage.Controllers
{
    [Area("EduHomeManage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInResult;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInResult)
        {

            _userManager = userManager;
            _roleManager = roleManager;
            _signInResult = signInResult;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                return View();
            }
            if (!user.IsAdmin)
            {
                //parolu duz de yazsa sehv de yazsa admin olmayanlar ucun error 
                ModelState.AddModelError("", "Username or Password is wrong");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInResult.PasswordSignInAsync(user, login.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password is wrong");
                return View();
            }
            return RedirectToAction("Index", "Dashboard");
        }
        //public async Task CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));
        //}

        //public async Task CreateAdmin()
        //{
        //    AppUser user = new AppUser
        //    {
        //        UserName = "Test",
        //        Email = "test@gmail.com",
        //        Fullname = "Test"
        //    };
        //    await _userManager.CreateAsync(user, "test12345");
        //    await _userManager.AddToRoleAsync(user, "SuperAdmin");
        //}
    }
}
