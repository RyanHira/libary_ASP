using LibaryASP_MVC.Data;
using LibaryASP_MVC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LibaryASP_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<LibaryDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("LibaryDbConnectionString")));

            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

			builder.Services.AddScoped<IitemRepository, ItemRepository>();

			builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository > ();



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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}