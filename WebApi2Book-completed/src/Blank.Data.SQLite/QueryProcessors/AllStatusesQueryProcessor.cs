// AllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using Blank.Data.SQLite.ModelServices;
using NHibernate;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;
using Blank.Data.SQLite.Bases.QueryProcessors;
using WebApi.Common.Implementations.Logging;

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
			var statuses = _modelServices.GettEntities<Blank.Data.Implementations.Entities.Status>();
			return statuses.Select(x => new Status() { StatusId = x.Id, Name = x.Name });
		}

		#region Protected Methods

		public override void InitLog()
		{
			modLog = LogService.GetLogService<AllStatusesQueryProcessor>();
		}

		#endregion
	}
}