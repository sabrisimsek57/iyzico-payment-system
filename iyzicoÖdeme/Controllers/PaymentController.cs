using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System;
using Iyzipay.Model;
using Iyzipay.Request;
using Iyzipay;

namespace iyzicoÖdeme.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
       



        [HttpPost]
        public IActionResult payadd([FromBody] PaymentModel paymentModel)
        {
            Options options = new Options();
            options.ApiKey = "sandbox";
            options.SecretKey = "sandbox";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = "10";
            request.PaidPrice = "12.0";
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            // Kullanıcıdan gelen kart bilgilerini PaymentCard ile dolduruyoruz
            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = paymentModel.CardHolderName;
            paymentCard.CardNumber = paymentModel.CardNumber;
            paymentCard.ExpireMonth = paymentModel.ExpireMonth;
            paymentCard.ExpireYear = paymentModel.ExpireYear;
            paymentCard.Cvc = paymentModel.Cvc;
            paymentCard.RegisterCard = 0; // Kart kaydetme seçeneği (0: kaydedilmez)
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = paymentModel.Name;
            buyer.Surname = paymentModel.Surname;
            buyer.GsmNumber = paymentModel.PhoneNumber;
            buyer.Email = paymentModel.Email;
            buyer.IdentityNumber = paymentModel.Tc;
            buyer.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = paymentModel.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = paymentModel.City;
            buyer.Country = "Turkey";
            buyer.ZipCode = paymentModel.ZipCode;
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = paymentModel.Name + " " + paymentModel.Surname;
            shippingAddress.City = paymentModel.City;
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = paymentModel.Address;
            shippingAddress.ZipCode = paymentModel.ZipCode;
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = paymentModel.Name + " " + paymentModel.Surname;
            billingAddress.City = paymentModel.City;
            billingAddress.Country = "Turkey";
            billingAddress.Description = paymentModel.Address;
            billingAddress.ZipCode = paymentModel.ZipCode;
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = "BI101";
            firstBasketItem.Name = "Binocular";
            firstBasketItem.Category1 = "Collectibles";
            firstBasketItem.Category2 = "Accessories";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = "8.0";
            basketItems.Add(firstBasketItem);

            BasketItem secondBasketItem = new BasketItem();
            secondBasketItem.Id = "BI102";
            secondBasketItem.Name = "Game code";
            secondBasketItem.Category1 = "Game";
            secondBasketItem.Category2 = "Online Game Items";
            secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            secondBasketItem.Price = "1.0";
            basketItems.Add(secondBasketItem);

            BasketItem thirdBasketItem = new BasketItem();
            thirdBasketItem.Id = "BI103";
            thirdBasketItem.Name = "Usb";
            thirdBasketItem.Category1 = "Electronics";
            thirdBasketItem.Category2 = "Usb / Cable";
            thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            thirdBasketItem.Price = "1.0";
            basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            Payment payment = Payment.Create(request, options);
            if (payment.Status == "success")
            {
                return Ok("ödeme başarılı olmuştur");
            }
            else
            {
                return BadRequest("Kart numarası geçersizdir"); // 400 Bad Request
            }

        }


    }
}
