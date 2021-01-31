using System.Collections.Generic;
using WebApp.Model.Entities;
using WebApp.Model.Requests;
using WebApp.Model.Results;

namespace WebApp.Business.Abstract.Services
{
    public interface ISampleService
    {
        IEnumerable<Sample> GetAll();
        Sample Get(string id);
        ServiceResult Add(AddSampleRequest request);
    }
}