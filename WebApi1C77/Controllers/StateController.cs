using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1C.Shared;
using WebApi1C77.Services;

namespace WebApi1C.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class StateController : ControllerBase
    {
        private readonly ILogger<StateController> _logger;
        private readonly Service1C77 _service1C77;
        public StateController(ILogger<StateController> logger, Service1C77 service1C77)
        {
            _logger = logger;
            _service1C77 = service1C77;
        }
        
 
        [HttpGet]
        public Service1C77State Get()
        {
            _logger.LogInformation("get state");
            return _service1C77.GetState();
        }
        [HttpPost]
        public async Task Post([FromBody] bool value)
        {
            if (value)
            {
                if (!_service1C77.IsStarted) await Task.Run(()=> _service1C77.Start());
            }
            else
            {
                if (_service1C77.IsStarted) await Task.Run(() => _service1C77.Stop());
            }
        }

    
    }
}
