using System.ComponentModel.DataAnnotations;

namespace ödemesistemi.Models
{
    public class CreateNewUserDto
    {

        [Required(ErrorMessage = "ad alanı gereklidir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "soyad alanı gereklidir")]
        public string Surname { get; set; }

        [Required( ErrorMessage = "Adres  alanı gereklidir.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Şehir  alanı gereklidir.")]
        public string City { get; set; }

        [Required( ErrorMessage = "Posta kodu alanı gereklidir.")]
        public string ZipCode { get; set; }

       
        [Required(ErrorMessage = "Geçersiz e-posta adresi alanı gereklidir.")]
        public string Email { get; set; }

    
        [Required(ErrorMessage = "Geçersiz telefon alanı gereklidir.")]
        public string PhoneNumber { get; set; }


        [Required( ErrorMessage = "TC kimlik numarası alanı gereklidir.")]
        public string Tc { get; set; }


        [Required(ErrorMessage = "kullanıcı adı alanı gereklidir")]
        public string Username { get; set; }
       
        [Required(ErrorMessage = "şifre  alanı gereklidir")]
        public string Password { get; set; }

        [Required(ErrorMessage = "şifre tekar alanı gereklidir")]
        [Compare("Password", ErrorMessage = "şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }
    }
}
