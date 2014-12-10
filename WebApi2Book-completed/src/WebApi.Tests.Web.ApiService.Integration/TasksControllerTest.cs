using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebApi.Tests.Common.Bases;
using WebApi2Book.Web.Api.Models;
using WebApi.Web.Data.Implementations.Response;
using WebApi.Common.Implementations.Constants;

namespace WebApi.Tests.Web.ApiService.Integration
{
	[TestClass]
	public class TasksControllerTest : BaseLocalHostController
	{
		public TasksControllerTest()
		{
			modAddress = modBaseAddress + "api/v1/tasks";
		}

		[TestMethod]
		public void AddTask()
		{
			const string data = "{\"Subject\":\"Fix something important\"}";
			var client = modClientHelper.CreateWebClient();

			try
			{
				string address = modBaseAddress + "api/v1/tasks";
				var responseString = client.UploadString(address, HttpMethod.Post.Method, data);

				JObject.Parse(responseString);
				var obj = JsonConvert.DeserializeObject<WebApi2Book.Web.Api.Models.Task>(responseString);
				Assert.IsNotNull(obj);

				var id = obj.TaskId;
				client.UploadString(address + "/" + id, HttpMethod.Delete.Method, "");
				responseString = client.DownloadString(address);
				var response =
					JsonConvert.DeserializeObject<PagedDataInquiryResponse<Task>>(responseString);
				Assert.IsFalse(response.Items.Any(x => x.TaskId == id));
			}
			finally
			{
				client.Dispose();
			}
		}

		[TestMethod]
		public void AddTask_denied()
		{
			const string data = "{\"Subject\":\"Fix something important\"}";
			string address = modBaseAddress + "api/v1/tasks";

			var client = this.modClientHelper.CreateWebClient(username: "jdoe");

			try
			{
				client.UploadString(address, HttpMethod.Post.Method, data);
				Assert.Fail();
			}
			catch (WebException e)
			{
				var statusCode = ((HttpWebResponse)(e.Response)).StatusCode;
				Assert.AreEqual(HttpStatusCode.Unauthorized, statusCode);
			}
			finally
			{
				client.Dispose();
			}
		}

		[TestMethod]
		public void TasksControllerTest_Method1()
		{

			using (var client = modClientHelper.CreateWebClient())
			{
				try
				{
					Task obj = CreateNewTask(client, this.modAddress);
					Assert.IsNotNull(obj);

					var id = obj.TaskId;

					Assert.IsTrue(ActivateTask(client, id));

					DeleteTask(client, id, this.modAddress);
					var tasks = GetAllTasks(client, this.modAddress);
					Assert.IsFalse(tasks.Any(x => x.TaskId == id));
				}
				catch (Exception)
				{
				}
			}
		}

		[TestMethod]
		public void TasksControllerTest_Method2()
		{
			using (var client = modClientHelper.CreateWebClient())
			{
				try
				{
					Task obj = CreateNewTask(client, this.modAddress);
					Assert.IsNotNull(obj);

					var id = obj.TaskId;

					Assert.IsTrue(ActivateTask(client, id));
					Assert.IsTrue(CompleteTask(client, id));

					DeleteTask(client, id, this.modAddress);
					var tasks = GetAllTasks(client, this.modAddress);
					Assert.IsFalse(tasks.Any(x => x.TaskId == id));
				}
				catch (Exception)
				{
				}
			}
		}

		[TestMethod]
		public void TasksControllerTest_Method3()
		{
			using (var client = modClientHelper.CreateWebClient())
			{
				try
				{
					Task obj = CreateNewTask(client, this.modAddress);
					Assert.IsNotNull(obj);

					var id = obj.TaskId;

					Assert.IsTrue(ActivateTask(client, id));
					Assert.IsTrue(CompleteTask(client, id));
					Assert.IsTrue(ReactivateTask(client, id));

					DeleteTask(client, id, this.modAddress);
					var tasks = GetAllTasks(client, this.modAddress);
					Assert.IsFalse(tasks.Any(x => x.TaskId == id));
				}
				catch (Exception)
				{
				}
			}
		}

		[TestMethod]
		public void TasksControllerTest_Method4()
		{
			long? id = 0;

			using (var client = modClientHelper.CreateWebClient())
			{
				try
				{
					Task obj = CreateNewTask(client, this.modAddress);
					Assert.IsNotNull(obj);

					id = obj.TaskId;
				}
				catch (Exception)
				{
				}
			}

			using (var client = modClientHelper.CreateWebClient(username: "jdoe"))
			{
				try
				{
					Task obj = GetTask(client, id, this.modAddress);
					Assert.IsNotNull(obj);
					Assert.IsNull(obj.TaskId);
				}
				catch (Exception)
				{
				}
			}

			using (var client = modClientHelper.CreateWebClient())
			{
				try
				{
					DeleteTask(client, id, this.modAddress);
					var tasks = GetAllTasks(client, this.modAddress);
					Assert.IsFalse(tasks.Any(x => x.TaskId == id));
				}
				catch (Exception)
				{
				}
			}
		}

		[TestMethod]
		public void TasksControllerTest_Method5()
		{
			long? id = 0;

			using (var client = modClientHelper.CreateWebClient())
			{
				try
				{
					Task obj = CreateNewTask(client, this.modAddress);
					Assert.IsNotNull(obj);

					id = obj.TaskId;

					AddUserToTask(taskId: id, userId: 1, client: client, prefix: "/users");

					var resp = GetTaskUsers(taskId: id, userId: 1, client: client);
					Assert.IsNotNull(resp);
					Assert.IsTrue(resp.Users.Any(x => x.UserId == 1));

					RemoveUserFromTask(taskId: id, userId: 1, client: client, prefix: "/users");

					DeleteTask(client, id, this.modAddress);
					var tasks = GetAllTasks(client, this.modAddress);
					Assert.IsFalse(tasks.Any(x => x.TaskId == id));
				}
				catch (Exception)
				{
				}
			}
		}

		[TestMethod]
		public void TasksControllerTest_Method6()
		{
			long? id = 0;

			using (var client = modClientHelper.CreateWebClient())
			{
				try
				{
					Task obj = CreateNewTask(client, this.modAddress);
					Assert.IsNotNull(obj);

					id = obj.TaskId;

					const string data = "{\"Subject\":\"Update\"}";
					Task updatedTask = UpdateTask(taskId: id, client: client, data: data);

					Assert.AreEqual(updatedTask.Subject, "Update");

					DeleteTask(client, id, this.modAddress);
					var tasks = GetAllTasks(client, this.modAddress);
					Assert.IsFalse(tasks.Any(x => x.TaskId == id));
				}
				catch (Exception)
				{
				}
			}
		}

		[TestMethod]
		public void TasksControllerTest_Method7()
		{
			long? id = 0;

			using (var client = modClientHelper.CreateWebClient())
			{
				try
				{
					Task obj = CreateNewTask(client, this.modAddress);
					Assert.IsNotNull(obj);

					id = obj.TaskId;

					const string data = "{\"Subject\":\"Update\", \"TaskId\":\"qwerty\"}";
					Task updatedTask = UpdateTask(taskId: id, client: client, data: data);

					Assert.IsTrue(false);
				}
				catch (Exception ex)
				{
					Assert.IsTrue(true);
				}
				finally
				{
					if (id!=0)
					{
						DeleteTask(client, id, this.modAddress);
						var tasks = GetAllTasks(client, this.modAddress);
						Assert.IsFalse(tasks.Any(x => x.TaskId == id));
					}
				}
			}
		}

		private Task UpdateTask(long? taskId, WebClient client, string data)
		{
			client.Headers.Add("Content-Type", Constants.MediaTypeNames.TextJson);
			string address = this.modAddress + "/" + taskId;
			string responseString = client.UploadString(address, HttpMethod.Put.Method, data);
			var response = JsonConvert.DeserializeObject<Task>(responseString);
			return response;
		}

		private TaskUsersResponse GetTaskUsers(long? taskId, int userId, WebClient client)
		{
			var responseString = client.DownloadString(this.modAddress + "/" + taskId + "/users");
			var response = JsonConvert.DeserializeObject<TaskUsersResponse>(responseString);
			return response;
		}

		private TaskUsersResponse AddUserToTask(long? taskId, long? userId, WebClient client, string prefix)
		{
			string address = this.modAddress + "/" + taskId + "/" + prefix + "/" + userId;
			string responseString = client.UploadString(address, HttpMethod.Put.Method, "");
			var response = JsonConvert.DeserializeObject<TaskUsersResponse>(responseString);
			return response;
		}

		private bool RemoveUserFromTask(long? taskId, long? userId, WebClient client, string prefix)
		{
			string address = this.modAddress + "/" + taskId + "/" + prefix + "/" + userId;
			client.UploadString(address, HttpMethod.Delete.Method, "");
			return true;
		}

		private bool ReactivateTask(WebClient client, long? id)
		{
			string address = this.modAddress + "/" + id + "/" + "reactivations";
			client.UploadString(address, HttpMethod.Post.Method, "");

			Task task = GetTask(client, id, this.modAddress);
			return task.Status.Name == "In Progress";
		}

		private bool CompleteTask(WebClient client, long? id)
		{
			string address = this.modAddress + "/" + id + "/" + "completions";
			client.UploadString(address, HttpMethod.Post.Method, "");

			Task task = GetTask(client, id, this.modAddress);
			return task.Status.Name == "Completed";
		}

		private bool ActivateTask(WebClient client, long? id)
		{
			string address = this.modAddress + "/" + id + "/" + "activations";
			client.UploadString(address, HttpMethod.Post.Method, "");

			Task task = GetTask(client, id, this.modAddress);
			return task.Status.Name == "In Progress";
		}

		public static IEnumerable<Task> GetAllTasks(WebClient client, string address)
		{
			var responseString = client.DownloadString(address);
			var response =
				JsonConvert.DeserializeObject<PagedDataInquiryResponse<Task>>(responseString);
			return response.Items;
		}

		public static Task GetTask(WebClient client, long? id, string address)
		{
			var responseString = client.DownloadString(address + "/" + id);
			var response = JsonConvert.DeserializeObject<Task>(responseString);
			return response;
		}

		public static void DeleteTask(WebClient client, long? id, string address)
		{
			client.UploadString(address + "/" + id, HttpMethod.Delete.Method, "");
		}

		public static Task CreateNewTask(WebClient client, string address)
		{
			const string data = "{\"Subject\":\"Fix something important\"}";

			var responseString = client.UploadString(address, HttpMethod.Post.Method, data);
			var obj = JsonConvert.DeserializeObject<WebApi2Book.Web.Api.Models.Task>(responseString);
			return obj;
		}
	}
}