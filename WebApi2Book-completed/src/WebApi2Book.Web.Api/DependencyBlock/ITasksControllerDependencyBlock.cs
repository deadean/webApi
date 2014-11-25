﻿// ITasksControllerDependencyBlock.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi.Data.QueryProcessors;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;

namespace WebApi2Book.Web.Api.DependencyBlock
{
    public interface ITasksControllerDependencyBlock
    {
        IAddTaskMaintenanceProcessor AddTaskMaintenanceProcessor { get; }
        ITaskByIdInquiryProcessor TaskByIdInquiryProcessor { get; }
        IUpdateTaskMaintenanceProcessor UpdateTaskMaintenanceProcessor { get; }
        IPagedDataRequestFactory PagedDataRequestFactory { get; }
        IAllTasksInquiryProcessor AllTasksInquiryProcessor { get; }
        IDeleteTaskQueryProcessor DeleteTaskQueryProcessor { get; }
    }
}