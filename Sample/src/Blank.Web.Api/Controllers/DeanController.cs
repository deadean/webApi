using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blank.Web.Api.Controllers
{
    public class DeanController : ApiController
    {
        [Route("api/dean/{id:int}")]
        public string Get(int id)
        {
            return "In the Get(int id) overload, id=" + id;
        }
    }
}
