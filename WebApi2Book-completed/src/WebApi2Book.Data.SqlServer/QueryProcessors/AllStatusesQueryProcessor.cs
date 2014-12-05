// AllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi.Data.QueryProcessors;
using System;

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
					_session.SaveOrUpdate(status);
					return status;
				}

				public bool RemoveStatus(string id)
				{
					var status = _session.Get<Status>(Convert.ToInt64(id));
					if (status != null)
					{
						_session.Delete(status);
						return true;
					}
					return false;
				}
		}
}