using Microsoft.Extensions.Logging;
using WebApp.Data.Abstract.Factories;
using WebApp.Data.Abstract.Repositories;
using WebApp.Model.Entities;

namespace WebApp.Data.Repositories
{
    public class SampleRepository : BaseRepository<Sample>, ISampleRepository
    {
        public SampleRepository(IDbConnectionFactory factory, ILogger<BaseRepository<Sample>> logger)
            : base(factory, logger)
        {
        }
    }
}