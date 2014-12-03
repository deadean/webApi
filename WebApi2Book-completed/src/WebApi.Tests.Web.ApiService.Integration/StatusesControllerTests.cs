using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Tests.Common.Bases;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;
using System.Linq;
using WebApi.Web.Data.Implementations.Response;
using Newtonsoft.Json.Linq;

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
			this.RunTest(() =>
			{
				const string dataAddNewStatus = "{\"NameStatus\":\"Status 001_Test\"}";
				string address = this.modBaseAddress + "api/v1/statuses";

				using (var client = modClientHelper.CreateWebClient())
				{
					var responseString = client.UploadString(address, HttpMethod.Post.Method, dataAddNewStatus);

					StatusResponse response_ = JsonConvert.DeserializeObject<StatusResponse>(responseString);
					Assert.IsTrue(response_.Name.Contains("001"));
					var id = response_.Id;

					responseString = client.UploadString(address + "/" + id, HttpMethod.Delete.Method, "");

					HttpResponseMessage response = this.modClient.GetAsync(address).Result;
					Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

					responseString = response.Content.ReadAsStringAsync().Result;
					StatusesResponse response__ = JsonConvert.DeserializeObject<StatusesResponse>(responseString);

					Assert.IsFalse(response__.Items.Any(x => x.Id.ToString() == id));

				}
			});
		}
	}
}
