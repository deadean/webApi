// IAllTasksQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using WebApi.Data.Implementations.Requests;
using WebApi2Book.Data;
using WebApi2Book.Data.Entities;

namespace WebApi.Data.QueryProcessors
{
    public interface IAllTasksQueryProcessor
    {
        QueryResult<Task> GetTasks(PagedDataRequest requestInfo);
    }
}