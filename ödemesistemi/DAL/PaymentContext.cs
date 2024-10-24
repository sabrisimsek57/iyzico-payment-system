
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ödemesistemi.Entites;

namespace ödemesistemi.DAL
{
    public class PaymentContext : IdentityDbContext<AppUser, AppRole, int>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-J1MPADH\\SABRI;database=ödemesistemi;integrated security=true");

        }
    }
}
