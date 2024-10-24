using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ödemesistemi.Entites;
using ödemesistemi.Models;

namespace ödemesistemi.Controllers
{
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CreateNewUserDto createNewUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var appUser = new AppUser()
            {
                Name = createNewUserDto.Name,
                Surname = createNewUserDto.Surname,
                Address = createNewUserDto.Address,
                City = createNewUserDto.City,
                ZipCode = createNewUserDto.ZipCode,
                Email = createNewUserDto.Email,
                PhoneNumber = createNewUserDto.PhoneNumber,
                Tc = createNewUserDto.Tc,
                UserName = createNewUserDto.Username 
            };

            var result = await _userManager.CreateAsync(appUser, createNewUserDto.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }
    }
}
