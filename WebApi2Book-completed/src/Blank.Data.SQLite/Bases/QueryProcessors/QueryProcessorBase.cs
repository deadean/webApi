﻿using Blank.Data.SQLite.ModelServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common.Enumerations;
using WebApi.Common.Implementations.Logging;
using WebApi.Common.Implementations.Unity;
using WebApi.Common.Interfaces.Logging;

namespace Blank.Data.SQLite.Bases.QueryProcessors
{
	public class QueryProcessorBase : WebApi.Common.Bases.QueryProcessors.QueryProcessorBase
	{
		#region Fields

		protected IModelServices modModelService;

		#endregion

		static QueryProcessorBase()
		{
			Container.RegisterType(typeof(IModelServices), typeof(Blank.Data.SQLite.ModelServices.ModelServices), enTypeLifeTime.Singleton);
		}

		public QueryProcessorBase()
		{
			try
			{
				InitLog();

				modModelService = Container.Resolve<IModelServices>();
			}
			catch (Exception ex)
			{
				modLog.Debug(ex.Message);
			}
		}

		public override  void InitLog()
		{
			modLog = LogService.GetLogService<QueryProcessorBase>();
		}

	}
}
