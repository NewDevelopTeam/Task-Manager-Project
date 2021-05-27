using System;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.FileProviders;
using PlusDashData.Data;
using TaskManager.Services.WebClients.Interfaces;
using TaskManager.Services.WebClients.Implementations;
using TaskManager.Moduls;

namespace TaskManager
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AccountContext>(options =>
                options.UseSqlServer(connection));

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options => 
                    {
                        options.Cookie.Name = "user_session";
                        options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
                        options.SessionStore = new CustomTicketStore(services);
                        options.ExpireTimeSpan = TimeSpan.FromHours(24);
                    });

            services.AddControllersWithViews();
            services.AddHttpClient<IAccountsWebClient, AccountsWebClient>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                      Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
                    RequestPath = new PathString("/vendor")
                });
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Pages}/{action=Boards}/{id?}");
            });
        }
    }
}
