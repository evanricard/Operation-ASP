using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Identity.Core;

namespace OperationASP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;  
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();


            services.AddIdentityCore<CoreUser>(options =>
            {
                //ok
            });


            /// AddScoped() services are created once per client request | inject the service into the Invoke or InvokeAsync method.
            services.AddScoped<IUserStore<CoreUser>, CoreUserStore>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("Cookies", options => options.LoginPath = "/Home/Login");

            //services.AddMvc().AddRazorRuntimeCompilation();
        }

        // Use this method to configure the HTTP request pipeline at runtime.
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


            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            //begin routing
            app.UseEndpoints(endpoints =>
            {
                //Can also use MapDynamicControllerRoute<DynamicRouteValueTransformer>()
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                                                                    /*.RequireAuthorization();*/

            });

            //env.

        }
    }
}
