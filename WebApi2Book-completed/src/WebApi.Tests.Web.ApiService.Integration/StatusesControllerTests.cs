using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Tests.Common.Bases;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using WebApi.Web.Data.Implementations.Response;

namespace WebApi.Tests.Web.ApiService.Integration.LocalHost
{
	[TestClass]
	public class StatusesControllerTests : BaseLocalHostController
	{
		[TestMethod]
		public void StatusesControllerTests_TestMethod1()
		{
			HttpResponseMessage response = this.modClient.GetAsync("api/v1/statuses").Result;

			Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
			var responseString = response.Content.ReadAsStringAsync().Result;
			var response_ = JsonConvert.DeserializeObject<StatusesResponse>(responseString);
			Assert.IsTrue(response_.Items.Any());
		}

		[TestMethod]
		public void StatusesControllerTests_TestMethod2()
		{
			//HttpResponseMessage response = this.modClient.GetAsync("api/v1/addresses/1410291328341454192148/directions/TS/stopps/?time=2014-11-11T06:30:36").Result;

			//Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

			//var responseString = response.Content.ReadAsStringAsync().Result;
			//var response_ = JsonConvert.DeserializeObject<RoutesStoppsResponseHeaderForSchool>(responseString);
			//Assert.IsTrue(response_.BusStopps.Any());
			//Assert.IsTrue(response_.BusStopps.Count() == 1);
			//Assert.AreEqual(response_.BusStopps.First().BusStopName, "Visby, Stenkumlaväg, 37");
		}
		
	}
}
