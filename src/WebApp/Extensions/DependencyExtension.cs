using Microsoft.Extensions.DependencyInjection;
using WebApp.Business.Abstract.Services;
using WebApp.Business.Services;
using WebApp.Data.Abstract.Factories;
using WebApp.Data.Abstract.Repositories;
using WebApp.Data.Factories;
using WebApp.Data.Repositories;

namespace WebApp.Extensions
{
    internal static class DependencyExtension
    {
        internal static void AddDependencyResolvers(this IServiceCollection services)
        {
            services.AddScoped<IDbConnectionFactory, MongoDbConnectionFactory>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ISampleRepository, SampleRepository>();
            services.AddScoped<ISampleService, SampleService>();
        }
    }
}