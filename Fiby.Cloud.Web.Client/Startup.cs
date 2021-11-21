using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiby.Cloud.Web.Client
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
            services.AddControllersWithViews();
            

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

            Register.IoCRegister.AddRegistration(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions()
            {
                OnPrepareResponse = context => {
                    context.Context.Response.Headers.Add("Cache-Control", "no-cache, no-store");
                    context.Context.Response.Headers.Add("Caduca", "-1");
                }
            });

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

            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors
           (builder =>
           {
               builder.SetIsOriginAllowed(_ => true)
               .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
           });
            app.UseAuthentication();
            app.UseAuthorization();
           

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
