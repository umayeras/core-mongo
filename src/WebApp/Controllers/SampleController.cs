using Microsoft.AspNetCore.Mvc;
using WebApp.Business.Abstract.Services;
using WebApp.Model.Entities;
using WebApp.Model.Requests;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SampleController : ControllerBase
    {
        private readonly ISampleService sampleService;

        public SampleController(ISampleService sampleService)
        {
            this.sampleService = sampleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(sampleService.GetAll());
        }
        
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(sampleService.Get(id));
        }

        [HttpPost]
        public IActionResult Post(AddSampleRequest request)
        {
            var result = sampleService.Add(request);
            return Ok(result.Message);
        }
    }
}