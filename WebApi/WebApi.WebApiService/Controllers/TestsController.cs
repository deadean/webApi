using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Web.Common.Validation;
using WebApi.Web.Data.Implementations;

namespace WebApi.WebApiService.Controllers
{
    public class TestsController : ApiController
    {
        // GET: api/Tests
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value9" };
        }

        // GET: api/Tests/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Tests
        [HttpPost]
        [ValidateModel]
        public string Post(HttpRequestMessage value, TestEntity inputValue)
        {
            return inputValue.ToString();
        }

        // PUT: api/Tests/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tests/5
        public void Delete(int id)
        {
        }
    }
}
