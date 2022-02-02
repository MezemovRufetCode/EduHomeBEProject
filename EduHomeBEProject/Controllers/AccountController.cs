using EduHomeBEProject.Models;
using EduHomeBEProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHomeBEProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
                return View();
            AppUser user = new AppUser
            {
                Email = register.Email,
                UserName = register.Username,
                Fullname = register.Fullname
            };
            if (!register.Terms)
            {
                ModelState.AddModelError("Terms", "Don't agree with Terms & Condition ?");
                return View();
            }
            //bu result startupdaki sertler duzdu ya yox onu yoxlayir
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, "Member");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid)
                return View();
            AppUser user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Passord is wrong");
                return View();
            }
            //bu result user login ola bilir ya yox odu

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, login.Remember, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account has been blocked for 5 minutes due to multiple login attempts");
                    return View();
                }
                ModelState.AddModelError("", "Username or Password is wrong");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM editedUser = new UserEditVM
            {
                Username = user.UserName,
                Email = user.Email,
                Fullname = user.Fullname
            };
            return View(editedUser);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM editedUser)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM eUser = new UserEditVM
            {
                Username = user.UserName,
                Email = user.Email,
                Fullname = user.Fullname
            };
            if (user.UserName != editedUser.Username && await _userManager.FindByNameAsync(editedUser.Username) != null)
            {
                ModelState.AddModelError("", $"{editedUser.Username} already existed");
                return View(eUser);
            }
            if (string.IsNullOrWhiteSpace(editedUser.CurrentPassword))
            {
                user.UserName = editedUser.Username;
                user.Email = editedUser.Email;
                user.Fullname = editedUser.Fullname;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.UserName = editedUser.Username;
                user.Email = editedUser.Email;
                user.Fullname = editedUser.Fullname;
                IdentityResult result = await _userManager.ChangePasswordAsync(user, editedUser.CurrentPassword, editedUser.Password);
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(eUser);
                }
            } 
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Show()
        {
            return Content(User.Identity.Name.ToString());
        }
    }
}
