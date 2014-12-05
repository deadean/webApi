// TasksControllerTest.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Net;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Tests.Common.Implementations;
using Newtonsoft.Json;
using WebApi.Tests.Common.Bases;
using WebApi2Book.Data.Entities;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.IntegrationTests
{
	[TestClass]
	public class TasksControllerTest : BaseLocalHostController
	{
		[TestMethod]
		public void AddTask()
		{
			const string data = "{\"Subject\":\"Fix something important\"}";
			var client = modClientHelper.CreateWebClient();

			try
			{
				string address = modBaseAddress + "tasks";
				var responseString = client.UploadString(address, HttpMethod.Post.Method, data);

				var jsonResponse = JObject.Parse(responseString);
				WebApi2Book.Web.Api.Models.Task obj = JsonConvert.DeserializeObject<WebApi2Book.Web.Api.Models.Task>(responseString);
				Assert.IsNotNull(obj);

				long? id = obj.TaskId;
				responseString = client.UploadString(address + "/" + id, HttpMethod.Delete.Method, "");
				responseString = client.DownloadString(address);
				PagedDataInquiryResponse<WebApi2Book.Web.Api.Models.Task> response__ = 
					JsonConvert.DeserializeObject<PagedDataInquiryResponse<WebApi2Book.Web.Api.Models.Task>>(responseString);
				Assert.IsFalse(response__.Items.Any(x => x.TaskId == id));

			}
			finally
			{
				client.Dispose();
			}
		}

		//[Test]
		//public void AddTask_denied()
		//{
		//		const string data = "{\"Subject\":\"Fix something important\"}";
		//		const string address = UriRoot + "tasks";

		//		var client = _webClientHelper.CreateWebClient(username: "jdoe");

		//		try
		//		{
		//				client.UploadString(address, HttpMethod.Post.Method, data);
		//				Assert.Fail();
		//		}
		//		catch (WebException e)
		//		{
		//				var statusCode = ((HttpWebResponse) (e.Response)).StatusCode;
		//				Assert.AreEqual(HttpStatusCode.Unauthorized, statusCode);
		//		}
		//		finally
		//		{
		//				client.Dispose();
		//		}
		//}
	}
}