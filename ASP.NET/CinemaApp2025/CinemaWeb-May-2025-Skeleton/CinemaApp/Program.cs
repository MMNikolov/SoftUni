using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Data;
namespace CinemaApp.Web
{
    using CinemaApp.Data;
    using CinemaApp.Services.Core;
    using CinemaApp.Services.Core.Interfaces;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<CinemaAppDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false; // Set to true if you want email confirmation
                options.Password.RequireDigit = false; // Set to true if you want to require digits in passwords
                options.Password.RequireLowercase = false; // Set to true if you want to require lowercase letters in passwords
                options.Password.RequireNonAlphanumeric = false; // Set to true if you want to require non-alphanumeric characters in passwords
                options.Password.RequireUppercase = false; // Set to true if you want to require uppercase letters in passwords
                options.Password.RequiredLength = 3; // Set the minimum length for passwords
            })
            .AddEntityFrameworkStores<CinemaAppDbContext>();

            builder.Services.AddScoped<IMovieService, MovieService>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
