using WebApp.Model.Requests;
using WebApp.Model.Results;

namespace WebApp.Business.Abstract.Services
{
    public interface ISampleService
    {
        ServiceDataResult GetAll();
        ServiceDataResult Get(string id);
        ServiceResult Add(AddSampleRequest request);
    }
}