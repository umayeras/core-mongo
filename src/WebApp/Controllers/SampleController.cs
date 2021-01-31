using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApp.Business.Abstract.Services;
using WebApp.Model.Constants;
using WebApp.Model.Requests;
using WebApp.Model.Results;
using WebApp.Validation.Abstract;

namespace WebApp.Controllers
{    [ApiController]
     [Route("[controller]")]
     public class SampleController : ControllerBase
     {
         private readonly ISampleService sampleService;
         private readonly IRequestValidator requestValidator;
 
         public SampleController(ISampleService sampleService, IRequestValidator requestValidator)
         {
             this.sampleService = sampleService;
             this.requestValidator = requestValidator;
         }
 
         [HttpGet]
         [ProducesResponseType(typeof(ServiceDataResult), (int) HttpStatusCode.OK)]
         public IActionResult Get()
         {
             return Ok(sampleService.GetAll());
         }
 
         [HttpGet("{id}")]
         [ProducesResponseType(typeof(ServiceDataResult), (int) HttpStatusCode.OK)]
         public IActionResult Get(string id)
         {
             return Ok(sampleService.Get(id));
         }
 
         [HttpPost]
         [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
         [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
         [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
         public IActionResult Post(AddSampleRequest request)
         {
             var validationResult = requestValidator.Validate(request);
             if (!validationResult.IsValid)
             {
                 return BadRequest(Messages.InvalidRequest);
             }
 
             var result = sampleService.Add(request);
 
             return !result.IsSuccess
                 ? Problem(Messages.GeneralError)
                 : Ok(result.Message);
         }
 
         [HttpPut]
         [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
         [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
         [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
         public IActionResult Put(UpdateSampleRequest request)
         {
             var validationResult = requestValidator.Validate(request);
             if (!validationResult.IsValid)
             {
                 return BadRequest(Messages.InvalidRequest);
             }
 
             var result = sampleService.Update(request);
 
             return !result.IsSuccess
                 ? Problem(Messages.GeneralError)
                 : Ok(result.Message);
         }
 
         [HttpDelete]
         [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
         [ProducesResponseType(typeof(string), (int) HttpStatusCode.BadRequest)]
         [ProducesResponseType(typeof(string), (int) HttpStatusCode.NotFound)]
         [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
         public IActionResult Delete(string id)
         {
             if (string.IsNullOrEmpty(id))
             {
                 return BadRequest(Messages.InvalidRequest);
             }
 
             var sample = sampleService.Get(id);
             if (sample.Data == null)
             {
                 return NotFound(Messages.NotFound);
             }
 
             var result = sampleService.Delete(id);
 
             return !result.IsSuccess
                 ? Problem(Messages.GeneralError)
                 : Ok(result.Message);
         }
     }

}