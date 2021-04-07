using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using V1C77.Models;
using WebApi1C.Shared;
using WebApi1C77.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi1C.Server.Controllers.V1C77
{
    [Route("[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ILogger<DocumentsController> _logger;
        private readonly Service1C77 _service1C77;

        public DocumentsController(ILogger<DocumentsController> logger, Service1C77 service1C77)
        {
            _logger = logger;
            _service1C77 = service1C77;
        }

        // GET: api/<DocumentsController>
        [HttpGet]
        public IActionResult Get(string documentName,DateTime? beginDate,DateTime? endDate,int top = 25)
        {
            if (string.IsNullOrWhiteSpace(documentName))
                throw new Exception("Try get catalog with out name");
            if (_service1C77.Metadata.Документы.TryGetValue(documentName, out var metadata))
            {
                DocumentFilter documentFilter = new DocumentFilter {BeginPeriod=beginDate,EndPeriod=endDate, Top=top};
                return Ok(_service1C77.GetDocumentItems(metadata,documentFilter));

            }
            else
                throw new Exception($"The catlog {documentName} not exist in current config!");

        }

        // GET api/<DocumentsController>/5
        //[HttpGet("{docName}")]
        //public string Get(string docName, DocumentSelector documentSelector)
        //{
        //    return "value";
        //}

        // POST api/<DocumentsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DocumentsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DocumentsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
