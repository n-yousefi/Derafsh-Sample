using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Derafsh.Business;
using DerafshSample.Core.Base.Identity;
using DerafshSample.Core.Concrete.Identity;
using DerafshSample.Services.Abstract;
using DerafshSample.Services.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DerafshSample
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
            services.AddMvc();
            // Get the connection string from appsettings.json file.
            string connectionString = Configuration.GetConnectionString("DerafshConnectionString");

            // Configure custom services to be used by the framework.
            var databaseConnectionService = new DatabaseConnectionService(connectionString);
            services.AddTransient<IDatabaseConnectionService>(
                e => databaseConnectionService
            );
            services.AddTransient<IDatabaseActions,DatabaseActions>();

            services.AddTransient<IIdentityRepository, IdentityRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IStateRepository, StateRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IPhoneRepository, PhoneRepository>();
            services.AddTransient<IEmailRepository, EmailRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
