using System.Linq;
using WebApp.Business.Abstract.Factories;
using WebApp.Business.Abstract.Services;
using WebApp.Data.Abstract.Repositories;
using WebApp.Model.Constants;
using WebApp.Model.Requests;
using WebApp.Model.Results;

namespace WebApp.Business.Services
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository repository;
        private readonly ISampleFactory factory;

        public SampleService(ISampleRepository repository, ISampleFactory factory)
        {
            this.repository = repository;
            this.factory = factory;
        }

        public ServiceDataResult GetAll()
        {
            var samples = repository.AsQueryable()
                .Select(p => new {p.Id, p.Title})
                .ToList();

            return ServiceDataResult.Success(samples);
        }

        public ServiceDataResult Get(string id)
        {
            var sample = repository.FindById(id);
            return ServiceDataResult.Success(sample);
        }

        public ServiceResult Add(AddSampleRequest request)
        {
            var sample = factory.CreateAddSample(request);
            var result = repository.Insert(sample);

            return string.IsNullOrEmpty(result)
                ? ServiceResult.Error(Messages.AddingFailed)
                : ServiceResult.Success(Messages.AddingSuccess);
        }
    }
}