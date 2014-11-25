// AllTasksQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Linq;
using Blank.Data.SQLite.ModelServices;
using NHibernate;
using WebApi.Data.Implementations.Requests;
using WebApi2Book.Data;
using WebApi2Book.Data.Entities;
using WebApi.Data.QueryProcessors;

namespace Blank.Data.SQLite.QueryProcessors
{
    public class AllTasksQueryProcessor : IAllTasksQueryProcessor
    {
        private readonly IModelServices _modelServices;

        public AllTasksQueryProcessor(ISession modelServices)
        {
            _modelServices = ModelServices.ModelServices.GetInstance();
        }

        public QueryResult<Task> GetTasks(PagedDataRequest requestInfo)
        {
            var query = _modelServices.GettEntities<Blank.Data.Implementations.Entities.Task>();

            var startIndex = ResultsPagingUtility.CalculateStartIndex(requestInfo.PageNumber, requestInfo.PageSize);

            var tasks = query.Skip(startIndex).Take(requestInfo.PageSize).ToList();

            var queryResult = new QueryResult<Task>(tasks.Select(x => new Task(){TaskId = x.Id,Subject = x.Subject}), query.Count, requestInfo.PageSize);

            return queryResult;
        }
    }
}