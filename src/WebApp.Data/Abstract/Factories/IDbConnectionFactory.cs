using MongoDB.Driver;

namespace WebApp.Data.Abstract.Factories
{
    public interface IDbConnectionFactory
    {
        IMongoDatabase OpenConnection();
    }
}