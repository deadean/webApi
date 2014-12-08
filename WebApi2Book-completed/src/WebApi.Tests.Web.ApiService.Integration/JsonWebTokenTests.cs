using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Tests.Common.Bases;
using JwtAuthForWebAPI;
using WebApi.Web.Common.Interfaces.Security;
using WebApi2Book.Web.Api.Models;

namespace WebApi.Tests.Web.ApiService.Integration
{
	[TestClass]
	public class JsonWebTokenTests : BaseLocalHostController
	{
		private string mvSymmetricKey;
		public JsonWebTokenTests()
		{
			mvSymmetricKey = modNinjectService.GetAll<IJsonSecurityTokenGenerator>().First().GetJWT();
			modAddress = modBaseAddress + "api/v1/tasks";
		}

		[TestMethod]
		public void JsonWebTokenTests_TestMethod1()
		{
			using (var client = modClientHelper.CreateWebClient(JWT:mvSymmetricKey))
			{
				try
				{
					Task obj = TasksControllerTest.CreateNewTask(client, this.modAddress);
					Assert.IsNotNull(obj);

					var id = obj.TaskId;

					TasksControllerTest.DeleteTask(client, id, this.modAddress);
					var tasks = TasksControllerTest.GetAllTasks(client, this.modAddress);
					Assert.IsFalse(tasks.Any(x => x.TaskId == id));
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
