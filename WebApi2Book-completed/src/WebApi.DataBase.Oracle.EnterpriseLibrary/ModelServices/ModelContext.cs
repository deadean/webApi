using System;
using System.Collections.Generic;
using System.Data.Common;
using Blank.Data.Implementations.Entities;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using WebApi.Common.Implementations.Factories;
using WebApi.Common.Interfaces.Factories;
using WebApi.Data.Implementations.Entities;

namespace WebApi.DataBase.Sqlite.EnterpriseLibrary.ModelServices
{
	internal sealed class ModelContext:IDisposable
	{
		private readonly DbConnection _connection;
		private readonly IObjectsByTypeFactory _factory = ObjectsByTypeFactory.GetFactory();
		private readonly Database _database;

		public ModelContext()
		{
			var configuration = new SystemConfigurationSource();
			var provider = new DatabaseProviderFactory(configuration);
			DatabaseFactory.SetDatabaseProviderFactory(provider);

			_database = (Database)DatabaseFactory.CreateDatabase("main");
			_connection = _database.CreateConnection();
			_connection.Open();

			_factory.RegisterObject<Status,IEnumerable<Status>>(GetStatuses);
		}

		public void Dispose()
		{
			_connection.Close();
		}

		public IEnumerable<T> Get<T>() where T : class, new()
		{
			return _factory.GetObjectFromFactory<T, IEnumerable<T>>();
		}

		private IEnumerable<Status> GetStatuses()
		{
			var mapper = MapBuilder<Status>.MapNoProperties()
								.Map(x => x.Id).ToColumn("Id")
								.Map(x => x.Name).ToColumn("Name")
								.Build();
			return _database.ExecuteSqlStringAccessor("SELECT * FROM Status", mapper);
		}
	}
}
