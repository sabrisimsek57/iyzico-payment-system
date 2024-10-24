using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ödemesistemi.Entites;
using ödemesistemi.Models;
using System.Net;
using System.Text;

namespace ödemesistemi.Controllers
{
    public class PaymenttController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly UserManager<AppUser> _userManager;

        public PaymenttController(UserManager<AppUser> userManager, HttpClient httpClient)
        {
            _userManager = userManager;
        
            _httpClient = httpClient;
        }



        int _id;
        
        
        [HttpGet]
        public async Task< IActionResult> Pay(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                // Kullanıcı bilgilerini tempdata'ya kaydet
                TempData["Name"] = user.Name;
                TempData["Surname"] = user.Surname;
                TempData["Address"] = user.Address;
                TempData["City"] = user.City;
                TempData["ZipCode"] = user.ZipCode;
                TempData["Email"] = user.Email;
                TempData["PhoneNumber"] = user.PhoneNumber;
                TempData["Tc"] = user.Tc;

                return View(); 
            }


            return View("Index","Login");
        }
        
        [HttpPost]
        public async Task<IActionResult> Pay(PayModel paymentModel)
        {


            if (ModelState.IsValid)
            {
                var jsonContent = JsonConvert.SerializeObject(paymentModel);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("https://localhost:7048/api/Payment", content);

                if (response.StatusCode == HttpStatusCode.OK)
                {

                   
                        return RedirectToAction("PaymentSuccess");
                    

                }
                else
                {
                    return RedirectToAction("PaymentFailed");
                }
            }

            return View(paymentModel);
        }

        // Ödeme başarılı olduğunda yönlendirilecek yeni sayfa
        [HttpGet]
        public IActionResult PaymentSuccess()
        {
            return View();
        }


        [HttpGet]
        public IActionResult PaymentFailed()
        {
            return View();
        }
        
    }
}
