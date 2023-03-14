using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjektSki.Data;
using System.Globalization;
using Microsoft.AspNetCore.Http;
using ProjektSki.DAL;
using ProjektSki.Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace ProjektSki
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MyAppData.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddSessionStateTempDataProvider();
            services.AddAuthentication("CookieAuthentication")
            .AddCookie("CookieAuthentication", config =>
            {
                config.Cookie.HttpOnly = true;
                config.Cookie.SecurePolicy = CookieSecurePolicy.None;
                config.Cookie.Name = "UserLoginCookie";
                config.LoginPath = "/Login/UserLogin";
                config.Cookie.SameSite = SameSiteMode.Strict;
                config.AccessDeniedPath = "/AccessDenied";  //przekierownaie przy access denied
            });
            /*
            //przekierowanie w momencie accessdenied
            services.Configure<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Events.OnRedirectToAccessDenied = new Func<RedirectContext<CookieAuthenticationOptions>, Task>(context =>
                   {
                       context.Response.Redirect(Configuration.GetSection("BaseAddresses:IdentityRazorPages").Value + "/AccessDenied");
                       return context.Response.CompleteAsync();
                   }
                );
            }
            );
            */
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireClaim("AdminNumber"));
            });
            //services.AddRazorPages();
            services.AddRazorPages(options => { 
                options.Conventions.AuthorizeFolder("/Producers");
                options.Conventions.AuthorizeFolder("/Categories");
                options.Conventions.AuthorizeFolder("/Admin");
                options.Conventions.AuthorizePage("/Products/Delete");
            });
            //wstrzykiwanie logowania
            services.Add(new ServiceDescriptor(typeof(ILoginDB), new LoginSqlDB(Configuration)));
            //wstrzykiwanie access denied
            services.AddTransient<IXMLService, XMLService>();
            services.AddDbContext<ShopContext>();
            services.AddSession();
            services.AddMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            }
            );

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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //middleware
            

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            //te dwie linijki dopisane odpowiadaja za autoryzacje
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMiddleware<MyMiddleware>();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        }
    }
    public static class MyAppData
    {
        public static IConfiguration Configuration;
    }
}
