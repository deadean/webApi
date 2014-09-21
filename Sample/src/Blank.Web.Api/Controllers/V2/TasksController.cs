using System.Net.Http;
using System.Web.Http;

namespace Blank.Web.Api.Controllers.V2
{
    [RoutePrefix("api/{appVersion:apiVersionConstraint(v2)}/tasks")]
    public class TasksController : ApiController
    {
        [Route("",Name="addTaskRouteV2")]
        [HttpPost]
        public Blank.Data.Implementations.Entities.Task AddTask(HttpRequestMessage requestMessage, Blank.Data.Implementations.Entities.Task newTask)
        {
            return new Blank.Data.Implementations.Entities.Task() { Subject = "In v2, newTask.Subject = " + newTask.Subject };
        }

        [Route("{id:int}")]
        public string Get(string taskNum)
        {
            return "In the Get(string taskNum) overload, taskNum=" + taskNum;
        }
    }
}
