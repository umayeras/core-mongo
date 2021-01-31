using System.Collections.Generic;
using WebApp.Business.Abstract.Factories;
using WebApp.Business.Abstract.Services;
using WebApp.Data.Abstract.Repositories;
using WebApp.Model.Constants;
using WebApp.Model.Entities;
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

        public IEnumerable<Sample> GetAll()
        {
            return repository.AsQueryable();
        }

        public Sample Get(string id)
        {
            return repository.FindById(id);
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