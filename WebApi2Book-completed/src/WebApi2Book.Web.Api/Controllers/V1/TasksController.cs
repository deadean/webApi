﻿// TasksController.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using log4net;
using System;
using System.IO;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Routing;
using WebApi2Book.Web.Common.Validation;

namespace WebApi2Book.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("tasks")]
    [UnitOfWorkActionFilter]
    //[Authorize(Roles = Constants.RoleNames.JuniorWorker)]
    [EnableCors("http://localhost:52976", "*", "*")]
    public class TasksController : ApiController
    {
        ILogManager log = new LogManagerAdapter();
        private readonly IAddTaskMaintenanceProcessor _addTaskMaintenanceProcessor;
        private readonly IAllTasksInquiryProcessor _allTasksInquiryProcessor;
        private readonly IDeleteTaskQueryProcessor _deleteTaskQueryProcessor;
        private readonly IPagedDataRequestFactory _pagedDataRequestFactory;
        private readonly ITaskByIdInquiryProcessor _taskByIdInquiryProcessor;
        private readonly IUpdateTaskMaintenanceProcessor _updateTaskMaintenanceProcessor;

        public TasksController(ITasksControllerDependencyBlock tasksControllerDependencyBlock)
        {
            try
            {
                _addTaskMaintenanceProcessor = tasksControllerDependencyBlock.AddTaskMaintenanceProcessor;
                _allTasksInquiryProcessor = tasksControllerDependencyBlock.AllTasksInquiryProcessor;
                _deleteTaskQueryProcessor = tasksControllerDependencyBlock.DeleteTaskQueryProcessor;
                _pagedDataRequestFactory = tasksControllerDependencyBlock.PagedDataRequestFactory;
                _taskByIdInquiryProcessor = tasksControllerDependencyBlock.TaskByIdInquiryProcessor;
                _updateTaskMaintenanceProcessor = tasksControllerDependencyBlock.UpdateTaskMaintenanceProcessor;
            }
            catch (Exception ex)
            {
                File.WriteAllText("log.txt", ex.ToString());
            }
        }

        public HttpResponseMessage Get()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("This is a V1 response.")
            };
        }

        [Route("", Name = "GetTasksRoute")]
        public PagedDataInquiryResponse<Task> GetTasks(HttpRequestMessage requestMessage)
        {
            var request = _pagedDataRequestFactory.Create(requestMessage.RequestUri);
            var tasks = _allTasksInquiryProcessor.GetTasks(request);
            return tasks;
        }

        [Route("{id:long}", Name = "GetTaskRoute")]
        public HttpResponseMessage GetTask(long id)
        {
            var task = _taskByIdInquiryProcessor.GetTask(id);
            //var task = new Task(){Subject = "hello"};
            //return task;
            return new HttpResponseMessage()
            {
                Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(task))
            };
        }

        [Route("", Name = "AddTaskRoute")]
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = Constants.RoleNames.Manager)]
        public IHttpActionResult AddTask(HttpRequestMessage requestMessage, NewTask newTask)
        {
            var task = _addTaskMaintenanceProcessor.AddTask(newTask);

            var result = new TaskCreatedActionResult(requestMessage, task);

            return result;
        }

        [Route("{id:long}", Name = "UpdateTaskRoute")]
        [HttpPut]
        [HttpPatch]
        [ValidateTaskUpdateRequest]
        [Authorize(Roles = Constants.RoleNames.SeniorWorker)]
        public Task UpdateTask(long id, [FromBody] object updatedTask)
        {
            var task = _updateTaskMaintenanceProcessor.UpdateTask(id, updatedTask);
            return task;
        }

        [Route("{id:long}", Name = "DeleteTaskRoute")]
        [HttpDelete]
        [Authorize(Roles = Constants.RoleNames.Manager)]
        public IHttpActionResult DeleteTask(long id)
        {
            _deleteTaskQueryProcessor.DeleteTask(id);
            return Ok();
        }
    }
}