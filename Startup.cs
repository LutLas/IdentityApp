using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityApp.Models;
using IdentityApp.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityApp
{
    public class Startup
    {
        public Startup(IConfiguration config) => Configuration = config;
        private IConfiguration Configuration { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddDbContext<ProductDbContext>(opts =>
            {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:AppDataConnection"]);
            });
            services.AddHttpsRedirection(opts =>
            {
                opts.HttpsPort = 44350;
            });
            services.AddDbContext<IdentityDbContext>(opts =>
            {
                opts.UseSqlServer(
                Configuration["ConnectionStrings:IdentityConnection"],
                opts => opts.MigrationsAssembly("IdentityApp")
                );
            });

            services.AddScoped<IEmailSender, ConsoleEmailSender>();

            services.AddIdentity<IdentityUser, IdentityRole>(it =>
            {
                it.SignIn.RequireConfirmedAccount = true;
                it.SignIn.RequireConfirmedEmail = true;
                it.Lockout.AllowedForNewUsers = true;
                it.Lockout.MaxFailedAccessAttempts = 3;
                it.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            })
            .AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();

            services.Configure<SecurityStampValidatorOptions>(it => {
                it.ValidationInterval = System.TimeSpan.FromMinutes(1);
            });

            services.AddScoped<TokenUrlEncoderService>();
            services.AddScoped<IdentityEmailService>();

            services.AddAuthentication()
            .AddFacebook(it =>
            {
                it.AppId = Configuration["Facebook:AppId"];
                it.AppSecret = Configuration["Facebook:AppSecret"];
            })
????        .AddGoogle(it =>
             {
                 it.ClientId = Configuration["Google:ClientId"];
                 it.ClientSecret = Configuration["Google:ClientSecret"];
             });

            services.ConfigureApplicationCookie(opts => {
                opts.LoginPath = "/Identity/SignIn";
                opts.LogoutPath = "/Identity/SignOut";
                opts.AccessDeniedPath = "/Identity/Forbidden";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapRazorPages();
            });
        }
    }
}
