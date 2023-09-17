using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyFirstWebSite.Sample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFirstWebSite.Sample.Controllers
{
    //todo  authorize eklenince maxquerystring hatası veriyo

    //[Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager=signInManager;
            _userManager=userManager;
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginVM() 
            {
                ReturnUrl= returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            if (!ModelState.IsValid)
                return View(loginVm);

            var user = await _userManager.FindByNameAsync(loginVm.UserName);

            if (user is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVm.ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    return RedirectToAction(loginVm.ReturnUrl);
                }
            }

            ModelState.AddModelError("", "User/Password not found");
            return View(loginVm);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser() { UserName = loginVM.UserName };
                var result=await _userManager.CreateAsync(user,loginVM.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(loginVM);
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
