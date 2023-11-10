using CeilInnHotelSystem.Configurations;
using CeilInnHotelSystem.Configurations.Mappers;
using CeilInnHotelSystem.Model;
using CeilInnHotelSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace CeilInnHotelSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddDbContext<CeilInnHotelDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddScoped<UserManager<AppUser>>();
            builder.Services.AddScoped<SignInManager<AppUser>>();
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<CeilInnHotelDbContext>() // Replace with your context
            .AddDefaultTokenProviders()
            .AddRoles<IdentityRole>().AddDefaultTokenProviders();
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Login";
                config.AccessDeniedPath = "/Forbidden";
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.BedTypeBedType
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
            app.UseApplicationDatabase<CeilInnHotelDbContext>(serviceProvider);
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.MapGet("/", () => Results.Redirect("/CustomerPage/Customer")); // Redirect to another page
            app.Run();
        }
    }
}