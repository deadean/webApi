// AllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi.Data.QueryProcessors;

namespace WebApi2Book.Data.SqlServer.QueryProcessors
{
    public class AllStatusesQueryProcessor : IAllStatusesQueryProcessor
    {
        private readonly ISession _session;

        public AllStatusesQueryProcessor(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Status> GetStatuses()
        {
            var statuses = _session.QueryOver<Status>().List();
            return statuses;
        }

				public Status AddStatus(Status status)
				{
					throw new System.NotImplementedException();
				}

				public bool RemoveStatus(string id)
				{
					throw new System.NotImplementedException();
				}
		}
}