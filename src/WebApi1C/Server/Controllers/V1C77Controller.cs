// Copyright (c)  Serhiy Lakas.
// Licensed under the MIT license.
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1C.Server.Services;
using WebApi1C.Shared;

namespace WebApi1C.Server.Controllers
{
    [Route("/api/v1c77")]
    public class V1C77Controller : ControllerBase
    {
        private readonly ILogger<V1C77Controller> _logger;
        private readonly Service1C77 _service1C77;
        public V1C77Controller(ILogger<V1C77Controller> logger, Service1C77 service1C77)
        {
            _logger = logger;
            _service1C77 = service1C77;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get(string selector, string key, bool fresh=false)
        {
            if (!_service1C77.IsStarted)
            {
                return Ok(new {Error= "Сервис не запущен!!!" });
            }
            
            if (string.IsNullOrWhiteSpace(selector))
            {
                return Ok(new string[] {"Metadata","Константа"});
            }

            try
            {
                return Ok(await _service1C77.GetDataFrom1C(selector, key));
            }
            catch (Exception e)
            {
                return Ok(new {Error = e.Message }); 
            }
        }
    
        [HttpGet("metadata")]
        public RootMetadata1C77 GetMetadata1C77()
        {
            return _service1C77.Metadata;
        }


        [HttpGet("state")]
        public Service1C77State GetState()
        {
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
