// AllStatusesQueryProcessor.cs
// Copyright Jamie Kurtz, Brian Wortman 2014.

using System.Collections.Generic;
using System.Linq;
using WebApi.Common.Implementations.Logging;
using WebApi.Common.Bases.QueryProcessors;
using WebApi2Book.Data.QueryProcessors;
using WebApi.DataBase.Sqlite.EnterpriseLibrary.ModelServices;
using WebApi2Book.Data.Entities;

namespace Blank.Data.SQLite.QueryProcessors
{
	public class AllStatusesQueryProcessor : QueryProcessorBase, IAllStatusesQueryProcessor
	{
		private readonly IModelServices _modelServices;

		public AllStatusesQueryProcessor()
		{
			_modelServices = new ModelServices();
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