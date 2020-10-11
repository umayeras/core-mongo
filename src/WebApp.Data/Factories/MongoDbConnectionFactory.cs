using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using WebApp.Data.Abstract.Factories;
using WebApp.Data.Constants;
using WebApp.Model.Constants;

namespace WebApp.Data.Factories
{
    public class MongoDbConnectionFactory : IDbConnectionFactory
    {
        private static IMongoDatabase database;
        private IConfiguration Configuration { get; }

        public MongoDbConnectionFactory(IConfiguration configuration)
        {
            Configuration = configuration;
            
            var config = Configuration.GetSection(SectionNames.MongoDbSettings).Get<MongoDbSettings>();
            var client = new MongoClient(config.Connection);
            database = client.GetDatabase(config.Database);
        }

        public IMongoDatabase OpenConnection()
        {
            return database;
        }
    }
}