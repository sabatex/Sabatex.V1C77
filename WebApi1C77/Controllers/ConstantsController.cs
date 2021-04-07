using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1C.Shared;
using WebApi1C77.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi1C77.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConstantsController : ControllerBase
    {
        private readonly ILogger<ConstantsController> _logger;
        private readonly Service1C77 _service1C77;
        public ConstantsController(ILogger<ConstantsController> logger, Service1C77 service1C77)
        {
            _logger = logger;
            _service1C77 = service1C77;
        }

        // GET: api/<ConstantsController>
        [HttpGet]
        public IActionResult Get()
        {
            if (_service1C77.IsStarted)
            return Problem();
            var result = new Dictionary<string,object>();
            foreach (var v in _service1C77.Metadata.Константы.Values)
            {
                try
                {
                    result.Add(v.Идентификатор,_service1C77.GetConstant(v.Идентификатор)); 
                }
                catch (Exception e)
                {
                    var s = e.Message;
                }
             }
            return Ok(result);
        }

        [HttpGet("metadata")]
        public Dictionary<string,ConstantMetadata1C77> GetMetadata()
        {
            return _service1C77.Metadata.Константы;
        }

        // GET api/<ConstantsController>/5
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            try
            {
                return  Ok(await Task.Run(()=>_service1C77.GetConstant(name)));
            }
            catch (Exception e)
            {
                return Ok(new { Error = e.Message}); 
            }
        }

        // PUT api/<ConstantsController>/5
        [HttpPut("{name}")]
        public void Put(string name, [FromBody] string value)
        {



        }
    }
}
