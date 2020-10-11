using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApp.Business.Abstract.Services;
using WebApp.Business.Services;
using WebApp.Data.Abstract.Factories;
using WebApp.Data.Abstract.Repositories;
using WebApp.Data.Factories;
using WebApp.Data.Repositories;

namespace WebApp
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory, MongoDbConnectionFactory>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ISampleRepository, SampleRepository>();
            services.AddScoped<ISampleService, SampleService>();
            services.AddHealthChecks();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Asp.Net Core WebApi with Mongo Db",
                    Description = "A sample project for Asp.Net Core WebApi with Mongo Db",
                    Contact = new OpenApiContact
                    {
                        Name = "Umay ERAS",
                        Email = "umayeras@hotmail.com",
                        Url = new Uri("http://umayeras.com"),
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asp.Net Core WebApi with Mongo Db V1");
            });
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}