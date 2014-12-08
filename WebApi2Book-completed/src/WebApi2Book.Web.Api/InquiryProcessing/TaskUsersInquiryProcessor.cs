// TaskUsersInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi.Common.Exceptions;
using WebApi.Data.QueryProcessors;
using WebApi.Web.Data.Implementations.Response;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public class TaskUsersInquiryProcessor : ITaskUsersInquiryProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly ITaskByIdQueryProcessor _queryProcessor;
        private readonly ITaskUsersLinkService _taskUsersLinkService;

        public TaskUsersInquiryProcessor(ITaskByIdQueryProcessor queryProcessor, IAutoMapper autoMapper,
            ITaskUsersLinkService taskUsersLinkService)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
            _taskUsersLinkService = taskUsersLinkService;
        }

        public TaskUsersResponse GetTaskUsers(long taskId)
        {
            var taskEntity = _queryProcessor.GetTask(taskId);
            if (taskEntity == null)
            {
                throw new RootObjectNotFoundException("Task not found.");
            }

            var task = _autoMapper.Map<Task>(taskEntity);

            var inquiryResponse = new TaskUsersResponse {TaskId = taskId, Users = task.Assignees};

            _taskUsersLinkService.AddLinks(inquiryResponse);

            return inquiryResponse;
        }
    }
}