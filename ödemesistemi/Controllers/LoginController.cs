using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ödemesistemi.Entites;
using ödemesistemi.Models;

namespace ödemesistemi.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;



        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(loginUserDto loginUserDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, false, false);
                if (result.Succeeded)
                {
                    // Giriş yapan kullanıcıyı al
                    var user = await _userManager.FindByNameAsync(loginUserDto.Username);
                    var userid = user.Id;
               

                    if (userid != null)
                    {





                        return RedirectToAction("Pay", "Paymentt", new { id = userid });
                    }
                    else
                    {
                        return RedirectToAction("Pay", " Paymentt");
                    }
                   

                }
                else
                {
                    // Giriş başarısızsa, uygun bir model ile Login görünümüne yönlendir
                    return View("Index");
                }

            }
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Login");
        }
    }
}
