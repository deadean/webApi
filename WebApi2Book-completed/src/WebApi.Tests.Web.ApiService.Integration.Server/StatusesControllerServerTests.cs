using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Tests.Common.Bases;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using WebApi.Web.Data.Implementations.Response;
using System.Linq;

namespace WebApi.Tests.Web.ApiService.Integration.Server
{
	[TestClass]
	public class StatusesControllerServerTests : BaseServerHostController
	{
		[TestMethod]
		public void StatusesControllerServerTests_TestMethod1()
		{
			this.RunTest(() =>
			{
				HttpResponseMessage response = this.modClient.GetAsync("api/v1/statuses").Result;

				Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
				var responseString = response.Content.ReadAsStringAsync().Result;
				var response_ = JsonConvert.DeserializeObject<StatusesResponse>(responseString);
				Assert.IsTrue(response_.Items.Any());
			});
		}
	}
}
