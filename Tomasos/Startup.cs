using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tomasos.BusinessLayer;
using Tomasos.Data;
using Tomasos.Models;
using Tomasos.Models.Identity;
using Tomasos.Services;

namespace Tomasos
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<TomasosContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<ITomasosService, TomasosService>();
            services.AddSingleton<IUserRepository, UserRepository>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireUppercase = false;

                    options.User.RequireUniqueEmail = true;
                }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serv)
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

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateUserRoles(serv).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serv)
        {
            var roleManager = serv.GetRequiredService<RoleManager<IdentityRole>>();
            var userManger = serv.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult roleResult;

            string[] roles = { "Admin", "PremiumUser", "RegularUser"};

            foreach (var role in roles)
            {
                var roleCheck = await roleManager.RoleExistsAsync(role);
                if (!roleCheck)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    if (!roleResult.Succeeded)
                    {
                        throw new Exception(roleResult.Errors.First().ToString());
                    }
                }

                ApplicationUser adminUser = await userManger.FindByEmailAsync("admin@local.test");
                ApplicationUser premiumUser = await userManger.FindByEmailAsync("premium@local.test");
                ApplicationUser regularUser = await userManger.FindByEmailAsync("user@local.test");

                await userManger.AddToRoleAsync(adminUser, "Admin");
                await userManger.AddToRoleAsync(premiumUser, "PremiumUser");
                await userManger.AddToRoleAsync(regularUser, "RegularUser");
            }
        }
    }
}
