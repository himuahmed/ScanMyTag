using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ScanMyTag.Data;
using ScanMyTag.Helpers;
using ScanMyTag.Models;
using ScanMyTag.Repository;
using ScanMyTag.Service;

namespace ScanMyTag
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ScanMyTagContext>(options => options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IQRGeneratorService, QRGeneratorService>();
            services.AddScoped<IQRCodeRepository, QRCodeRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserClaimsPrincipalFactory<UserModel>, AplicationUserClaimsPrincipalFactory>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddIdentity<UserModel, IdentityRole>().AddEntityFrameworkStores<ScanMyTagContext>().AddDefaultTokenProviders();
            services.AddHttpContextAccessor();

            //services.AddScoped<IStringToImageConverter, StringToImageConverter>();
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = _configuration["ApplicationSettings:LoginPath"];
            });

            services.Configure<IdentityOptions>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequireUppercase = false;
                option.Password.RequiredLength = 6;
                option.Password.RequireLowercase = false;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequiredUniqueChars = 0;
                option.SignIn.RequireConfirmedEmail = true;

            });
            services.Configure<SMTPModel>(_configuration.GetSection("SMTPConfig"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

            });
        }
    }
}
