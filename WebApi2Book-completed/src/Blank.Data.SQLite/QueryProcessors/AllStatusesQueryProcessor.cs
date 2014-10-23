// AllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using Blank.Data.SQLite.ModelServices;
using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace Blank.Data.SQLite.QueryProcessors
{
    public class AllStatusesQueryProcessor : IAllStatusesQueryProcessor
    {
        private readonly IModelServices _modelServices;

        public AllStatusesQueryProcessor(ISession session)
        {
            _modelServices = new ModelServices.ModelServices();
        }

        public IEnumerable<Status> GetStatuses()
        {
            var statuses = _modelServices.GettEntities<Blank.Data.Implementations.Entities.Status>();
            return statuses.Select(x => new Status(){StatusId = x.Id, Name = x.Name});
        }
    }
}