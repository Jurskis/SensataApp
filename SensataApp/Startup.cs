using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SensataApp.Hubs;
using SensataApp.Models;
using System.Linq;

namespace SensataApp
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
            services.AddDbContext<VehiclesContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("VehiclesContext")
                ));
            services.AddSignalR();

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, VehiclesContext vehiclesContext)
        {
            vehiclesContext.Database.Migrate();
            // Create a database with dummy data if it doesn't exist.
            if (!vehiclesContext.Vehicles.Any())
            {
                vehiclesContext.Vehicles.AddRange(
                    new Vehicle { Id = "17affb45-5ab3-4e04-b47f-20fa9455b3ea", Name = "Car_Audi" }, 
                    new Vehicle { Id = "6b97b98b-8cd7-49e8-bc8f-2f8788551f0a", Name = "Bike_BMW" },
                    new Vehicle { Id = "e03068a5-c7e9-4231-ae36-76860790e73f", Name = "Truck_Volvo" });
                vehiclesContext.VehicleInputs.AddRange(
                    new VehicleInput { VehicleId = "17affb45-5ab3-4e04-b47f-20fa9455b3ea", Latitude = 15.961329, Longitude = 76.581862, Speed = 120 },
                    new VehicleInput { VehicleId = "17affb45-5ab3-4e04-b47f-20fa9455b3ea", Latitude = 15.121402, Longitude = 76.546147, Speed = 145 },
                    new VehicleInput { VehicleId = "17affb45-5ab3-4e04-b47f-20fa9455b3ea", Latitude = 15.398271, Longitude = 76.623103, Speed = 70 },
                    new VehicleInput { VehicleId = "6b97b98b-8cd7-49e8-bc8f-2f8788551f0a", Latitude = 40.979898, Longitude = -2.572517, Speed = 150 },
                    new VehicleInput { VehicleId = "6b97b98b-8cd7-49e8-bc8f-2f8788551f0a", Latitude = 40.863680, Longitude = -2.011503, Speed = 120 },
                    new VehicleInput { VehicleId = "6b97b98b-8cd7-49e8-bc8f-2f8788551f0a", Latitude = 39.757880, Longitude = -0.274504, Speed = 180 },
                    new VehicleInput { VehicleId = "e03068a5-c7e9-4231-ae36-76860790e73f", Latitude = 64.174389, Longitude = -51.731006, Speed = 120 },
                    new VehicleInput { VehicleId = "e03068a5-c7e9-4231-ae36-76860790e73f", Latitude = 64.198309, Longitude = -51.626364, Speed = 110 },
                    new VehicleInput { VehicleId = "e03068a5-c7e9-4231-ae36-76860790e73f", Latitude = 64.238627, Longitude = -51.062906, Speed = 100 });
            }
            vehiclesContext.SaveChanges();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                endpoints.MapHub<VehicleInputHub>("/vehicleinputs");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
