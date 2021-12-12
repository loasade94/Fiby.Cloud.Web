using Fiby.Cloud.Web.Client.Filters;
using Fiby.Cloud.Web.Common.Helpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment)
        {
            _hostingEnvironment = webHostEnvironment;
            Configuration = configuration;
        }

        private IWebHostEnvironment _hostingEnvironment;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var keysDirectoryName = "Keys";
            var keysDirectoryPath = Path.Combine(_hostingEnvironment.ContentRootPath, keysDirectoryName);
            if (!Directory.Exists(keysDirectoryPath))
            {
                Directory.CreateDirectory(keysDirectoryPath);
            }
            services.AddDataProtection()
              .PersistKeysToFileSystem(new DirectoryInfo(keysDirectoryPath))
              .SetApplicationName("CustomCookieAuthentication");

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,o =>
            {
                o.Cookie.Name = "_auth";
                o.LoginPath = new PathString("/Account/Login");
                o.LogoutPath = new PathString("/Account/Login");
                o.Cookie.HttpOnly = true;
                o.ExpireTimeSpan = TimeSpan.FromDays(1);
                o.AccessDeniedPath = new PathString("/Account/Login");
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireClaim("Profile", "Admin"));
                //options.AddPolicy("Supervisor", policy => policy.RequireClaim("Profile", "Supervisor"));
                //options.AddPolicy("Worker", policy => policy.RequireClaim("Profile", "Worker"));
            });

            services.AddControllersWithViews();

            Register.IoCRegister.AddRegistration(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            ServiceActivator.Configure(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.Use(async (context, next) =>
            {
                try
                {
                    context.Response.GetTypedHeaders().CacheControl =
                     new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
                     {
                         Public = true,
                         MaxAge = TimeSpan.FromMinutes(60),
                         NoStore = true
                     };
                    await next();
                    //logger.LogInformation($"   context.Response.StatusCode: {context.Response.StatusCode} - context.Request.Host.Value {context.Request.Host.Value} - context.Request.Method: {context.Request.Method} - context.Request.Protocol: {context.Request.Protocol} - context.Request.Path.Value: {context.Request.Path.Value} - context.Request.RouteValues [action]: {context.Request.RouteValues["action"]} - context.Request.RouteValues [controller]: {context.Request.RouteValues["controller"]}");

                    if (context.Response.StatusCode != 200)
                    {
                        if (context.Response.StatusCode == 404 || context.Response.StatusCode == 500)
                        {
                            context.Request.Path = "/Home/Error";

                            //logger.LogInformation($"ERROR  context.Response.StatusCode: {context.Response.StatusCode} - context.Request.Host.Value {context.Request.Host.Value} - context.Request.Method: {context.Request.Method} - context.Request.Protocol: {context.Request.Protocol} - context.Request.Path.Value: {context.Request.Path.Value} - context.Request.RouteValues [action]: {context.Request.RouteValues["action"]} - context.Request.RouteValues [controller]: {context.Request.RouteValues["controller"]}");
                            await next();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //logger.LogInformation($"   context: {ex.Message}");
                }
            });


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseAzureAppConfiguration();
            

            //app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();

            app.UseCors
           (builder =>
           {
               builder.SetIsOriginAllowed(_ => true)
               .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
           });

            
            app.ConfigureExceptionHandler();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "Users",
                    pattern: "{area:exists}/{controller=Options}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
