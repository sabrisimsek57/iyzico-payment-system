using Microsoft.AspNetCore.Identity;

namespace ödemesistemi.Entites
{
    public class AppUser : IdentityUser<int>
    {

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Tc { get; set; }
    }
}
