using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.WebApiService.Controllers
{
    public class ValuesController : ApiController
    {
        public class NewTask
        {
            [Required(AllowEmptyStrings = false)]
            public string Subject { get; set; }

            public DateTime? StartDate { get; set; }

            public DateTime? DueDate { get; set; }
        }

        /// <summary>
        ///     Action filter to check the model state before the controller action is invoked.
        /// </summary>
        /// <remarks>
        ///     From http://www.asp.net/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
        /// </remarks>
        public class ValidateModelAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(HttpActionContext actionContext)
            {
                if (actionContext.ModelState.IsValid == false)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, actionContext.ModelState);
                }
            }

            /// <summary>
            /// To prevent filter from executing twice on same call. Problem solved by:
            /// http://stackoverflow.com/questions/18485479/webapi-filter-is-calling-twice?rq=1
            /// </summary>
            public override bool AllowMultiple
            {
                get { return false; }
            }
        }

        // GET: api/Values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value6" };
        }

        // GET: api/Values/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [ValidateModel]
        public string Post(HttpRequestMessage value, NewTask newTask)
        {
            return value.Content.ToString();
        }

        // PUT: api/Values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Values/5
        public void Delete(int id)
        {
        }
    }
}
