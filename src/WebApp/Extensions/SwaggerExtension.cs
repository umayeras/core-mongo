using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebApp.Extensions
{
    internal static class SwaggerExtension
    {
        internal static void AddSwaggerDocumentation(this IServiceCollection services)
        {
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

        internal static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Asp.Net Core WebApi with Mongo Db V1");
            });
        }
    }
}