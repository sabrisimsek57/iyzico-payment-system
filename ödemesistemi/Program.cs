using Microsoft.AspNetCore.Authentication.Cookies;
using ödemesistemi.DAL;
using ödemesistemi.Entites;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PaymentContext>();

builder.Services.AddIdentity<AppUser, AppRole>().AddEntityFrameworkStores<PaymentContext>();


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Register/Index"; // Giriþ yapýlmamýþsa yönlendirilecek sayfa
        options.LogoutPath = "/Login/Logout"; // Çýkýþ yapýldýktan sonra yönlendirilecek sayfa
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5); // Cookie'nin geçerlilik süresi
        options.SlidingExpiration = false; // Cookie'nin geçerliliði süresinin uzatýlmasý
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


app.UseAuthentication(); // Kimlik doðrulama middleware
app.UseAuthorization(); // Yetkilendirme middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}");

app.Run();
