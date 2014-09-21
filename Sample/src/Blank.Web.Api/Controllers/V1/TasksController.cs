using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Blank.Web.Common.Routing;
using Blank.Data;

namespace Blank.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    public class TasksController : ApiController
    {
        [Route("{id:int}")]
        [HttpGet]
        public string Get(int id)
        {
            return "In the Get(int id) overload, id=" + id;
        }

        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        public Blank.Data.Implementations.Entities.Task AddTask(HttpRequestMessage requestMessage, Blank.Data.Implementations.Entities.Task newTask)
        {
            return new Blank.Data.Implementations.Entities.Task() {Subject = "In v1, newTask.Subject = " + newTask.Subject};
        }

        //[Route("api/tasks/{id:int}")]
        //public string Get(int id)
        //{
        //    return "In the Get(int id) overload, id=" + id;
        //}

        //[Route("api/v1/dean/{id:int}")]
        //public string Get(int id)
        //{
        //    return "In the Get(int id) overload, id=" + id;
        //}

        //[Route("api/v1/tasks/{taskNum:alpha}")]
        //public string Get(string taskNum)
        //{
        //    return "In the Get(string taskNum) overload, taskNum=" + taskNum;
        //}

        //[Route("id:int:max(100)")]
        //public string GetTaskWithAMaxIdOf100(int id)
        //{
        //    return "In the GetTaskWithAMaxIdOf100(int id) method, id = " + id;
        //}

        //// Is doesnt start from Get, but it can approve http get requests
        //[Route("id:int:min(101)")]
        //[HttpGet]
        //public string FindTaskWithAMinIdOf100(int id)
        //{
        //    return "In the FindTaskWithAMinIdOf100(int id) method, id = " + id;
        //}
    }
}
