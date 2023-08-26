using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotosService.AsyncDataServices;
using PhotosService.Data;
using PhotosService.EventProcessing;
using PhotosService.SyncDataServices.Grpc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace PhotosService
{
    public class Startup
    {
     
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddDbContext<AppDbContext> (optionsAction =>
            optionsAction.UseInMemoryDatabase("InMemnam"));
            services.AddScoped<IPhotoRepo, PhotoRepo>(); //IF they ask for IUser Repo we give them user repo
            services.AddControllers();
            services.AddHostedService<MessageBusSubscriber>();
            services.AddSingleton<IEventProcessor, EventProcessor>(); //Singletone ->> all time alongside 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IDogDataClient, DogDataClient>();  //Registering it
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhotosService", Version = "v1" });
            });

     

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
          
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhotosService v1"));
            /*
            app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "swagger"; // Set the route prefix for the unified portal
    c.SwaggerEndpoint("/service1/swagger/v1/swagger.json", "Service 1 v1");
    c.SwaggerEndpoint("/service2/swagger/v1/swagger.json", "Service 2 v1");
    // Add more endpoints for other services
});
How to create docs for all my buddies:
services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceName", Version = "v1" });
    // Add any additional configuration here
});
            */

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                
            });
            PrepDb.PrepPopulation(app);
        }
    }
}