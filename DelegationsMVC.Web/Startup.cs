using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DelegationsMVC.Infrastructure;
using DelegationsMVC.Application;
using FluentValidation.AspNetCore;
using FluentValidation;
using DelegationsMVC.Application.ViewModels.EmployeeVm;
using DelegationsMVC.Application.ViewModels.DelegationVm;
using DelegationsMVC.Application.ViewModels.RouteVm;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Logging;
using DelegationsMVC.Web.Filters;

namespace DelegationsMVC.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Context>();
            services.AddApplication();
            services.AddInfrastructure();

            services.AddControllersWithViews()
                .AddFluentValidation(fv =>
                 {
                     fv.ImplicitlyValidateChildProperties = true;
                     fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                 });
            services.AddRazorPages();

            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequireLowercase = true;
                opt.Password.RequiredLength = 8;

                opt.SignIn.RequireConfirmedEmail = false;
                opt.User.RequireUniqueEmail = true;
            });

            services.AddAuthentication().AddGoogle(opt =>
           {
               IConfigurationSection googleAuthenSection = Configuration.GetSection("Authentication:Google");
               opt.ClientId = googleAuthenSection["ClientId"];
               opt.ClientSecret = googleAuthenSection["Secret"];
           });

            services.AddScoped<CheckContactPermission>();
            services.AddScoped<CheckVehiclePermission>();
            services.AddScoped<CheckDelegationPermission>();
            services.AddScoped<CheckRoutePermission>();
            services.AddScoped<CheckEmployeeDelegationPermission>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/myLog-{Date}.txt");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            var defaultCulture = new CultureInfo("pl-PL");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
