// DeleteTaskQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using NHibernate;
using WebApi.Data.QueryProcessors;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class DeleteTaskQueryProcessor : IDeleteTaskQueryProcessor
    {
        private readonly ISession _session;

        public DeleteTaskQueryProcessor(ISession session)
        {
            _session = session;
        }

        public void DeleteTask(long taskId)
        {
            var task = _session.Get<Task>(taskId);
            if (task != null)
            {
                _session.Delete(task);
            }
        }
    }
}