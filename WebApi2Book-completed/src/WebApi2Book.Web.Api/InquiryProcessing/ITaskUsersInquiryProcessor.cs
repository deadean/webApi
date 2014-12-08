// ITaskUsersInquiryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi.Web.Data.Implementations.Response;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.InquiryProcessing
{
    public interface ITaskUsersInquiryProcessor
    {
        TaskUsersResponse GetTaskUsers(long taskId);
    }
}