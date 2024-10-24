using Microsoft.AspNetCore.Authentication.Cookies;
using �demesistemi.DAL;
using �demesistemi.Entites;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaymentContext>();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<PaymentContext>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Register/Index"; // Giri� yap�lmam��sa y�nlendirilecek sayfa
        options.LogoutPath = "/Login/Logout"; // ��k�� yap�ld�ktan sonra y�nlendirilecek sayfa
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5); // Cookie'nin ge�erlilik s�resi
        options.SlidingExpiration = false; // Cookie'nin ge�erlili�i s�resinin uzat�lmas�
    });
builder.Services.AddAuthorization();

builder.Services.AddHttpClient();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication(); // Kimlik do�rulama middleware
app.UseAuthorization(); // Yetkilendirme middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
