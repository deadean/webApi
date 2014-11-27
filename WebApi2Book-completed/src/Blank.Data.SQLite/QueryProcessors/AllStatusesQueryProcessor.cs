// AllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using Blank.Data.SQLite.ModelServices;
using NHibernate;
using WebApi2Book.Data.Entities;
using Blank.Data.SQLite.Bases.QueryProcessors;
using WebApi.Data.QueryProcessors;

namespace Blank.Data.SQLite.QueryProcessors
{
	public class AllStatusesQueryProcessor : QueryProcessorBase, IAllStatusesQueryProcessor
	{
		private readonly IModelServices _modelServices;

		public AllStatusesQueryProcessor()
		{
			_modelServices = new ModelServices.ModelServices();
		}

		public IEnumerable<Status> GetStatuses()
		{
			var statuses = _modelServices.GettEntities<WebApi.Data.Implementations.Entities.Status>();
			return statuses.Select(x => new Status() { StatusId = x.Id, Name = x.Name });
		}

		#region Protected Methods

		public override void InitLog()
		{
			//modLog = LogService.GetLogService<AllStatusesQueryProcessor>();
		}

		#endregion

		public Status AddStatus(Status status)
		{
			_modelServices.AddNewEntity(new WebApi.Data.Implementations.Entities.Status() { Name = status.Name });
			status.StatusId = _modelServices.GettEntities<WebApi.Data.Implementations.Entities.Status>().FirstOrDefault(x => x.Name == status.Name).Id;
			return status;
		}
	}
}