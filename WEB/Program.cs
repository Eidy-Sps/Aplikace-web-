using Microsoft.AspNetCore.Authentication.Cookies;

namespace WEB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Přidání služeb pro MVC (Controllery a View)
            builder.Services.AddControllersWithViews();

            // Konfigurace přihlašování (Cookies)
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/User/Login";
                });

            builder.Services.AddAuthorization();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // --- TADY BYLA CHYBA: Chybělo načítání statických souborů (CSS, obrázky) ---
            app.UseStaticFiles();

            app.UseRouting();

            // Autentizace MUSÍ být před Autorizací
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}