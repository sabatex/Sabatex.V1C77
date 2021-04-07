using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1C77.Services;

namespace WebApi1C77.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        private readonly ILogger<MetadataController> _logger;
        private readonly Service1C77 _service1C77;
        public MetadataController(ILogger<MetadataController> logger, Service1C77 service1C77)
        {
            _logger = logger;
            _service1C77 = service1C77;
        }
        [HttpGet]
        public object Get()
        {
            if (_service1C77.IsStarted)
            {
                return _service1C77.Metadata;
            }
            else
                return _service1C77.MessageNotStarted;
        }

    }
}
