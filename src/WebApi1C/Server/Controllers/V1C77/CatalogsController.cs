using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using sabatex.V1C77.Models.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi1C.Server.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi1C.Server.Controllers.V1C77
{
    [Route("api/v1c77/[controller]")]
    [ApiController]
    public class CatalogsController : ControllerBase
    {
        private readonly ILogger<CatalogsController> _logger;
        private readonly Service1C77 _service1C77;

        public CatalogsController(ILogger<CatalogsController> logger, Service1C77 service1C77)
        {
            _logger = logger;
            _service1C77 = service1C77;
        }


        // GET: api/<ReferencesController>
        //[HttpGet]
        //public IActionResult Get([FromBody] string catalogName)
        //{
        //    if (string.IsNullOrWhiteSpace(catalogName))
        //        throw new Exception("Try get catalog with out name");
        //    if (_service1C77.Metadata.Справочники.TryGetValue(catalogName.ToLower(), out var metadata))
        //    {
        //        return Ok(_service1C77.GetCatalogItems(metadata));  

        //    }
        //    else
        //        throw new Exception($"The catlog {catalogName} not exist in current config!");
        //}
        //[HttpGet]
        //public IActionResult Get([FromBody] string catalogName)
        //{
        //    if (string.IsNullOrWhiteSpace(catalogName))
        //        throw new Exception("Try get catalog with out name");
        //    if (_service1C77.Metadata.Справочники.TryGetValue(catalogName.ToLower(), out var metadata))
        //    {
        //        return Ok(_service1C77.GetCatalogItems(metadata));

        //    }
        //    else
        //        throw new Exception($"The catlog {catalogName} not exist in current config!");
        //}
        [HttpGet]
        public IActionResult Get([FromQuery] string catalogName,[FromQuery] string catalogId)
        {
            if (string.IsNullOrWhiteSpace(catalogName))
                throw new Exception("Try get catalog with out name");
            if (_service1C77.Metadata.Справочники.TryGetValue(catalogName.ToLower(), out var metadata))
            {
                if (string.IsNullOrWhiteSpace(catalogId))
                {
                    return Ok(_service1C77.GetCatalogItems(metadata));
                }
                else
                {
                    return Ok(_service1C77.GetCatalogItem(catalogId,metadata));
                }
            }
            else
                throw new Exception($"The catlog {catalogName} not exist in current config!");

        }

        // GET api/<ReferencesController>/5
        //[HttpGet]
        //public IActionResult Get(string catalogName,[FromBody] string fieldName, string filter)
        //{
        //    return Ok();

        //}

        // POST api/<ReferencesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReferencesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReferencesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
