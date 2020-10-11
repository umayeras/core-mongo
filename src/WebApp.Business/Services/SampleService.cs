using System.Collections.Generic;
using WebApp.Business.Abstract.Services;
using WebApp.Data.Abstract.Repositories;
using WebApp.Model.Constants;
using WebApp.Model.Entities;
using WebApp.Model.Results;

namespace WebApp.Business.Services
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository repository;

        public SampleService(ISampleRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Sample> GetAll()
        {
            return repository.AsQueryable();
        }

        public Sample Get(string id)
        {
            return repository.FindById(id);
        }

        public ServiceResult Add(Sample sample)
        {
            var result = repository.Insert(sample);

            return string.IsNullOrEmpty(result)
                ? ServiceResult.Error(Messages.AddingFailed)
                : ServiceResult.Success(Messages.AddingSuccess);
        }
    }
}