using Blank.Data.Implementations.Entities;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DataBase.Sqlite.EnterpriseLibrary
{
    public class Connection
    {
        public void Connect()
        {
            var configuration = new SystemConfigurationSource();
            var provider = new DatabaseProviderFactory(configuration);
            DatabaseFactory.SetDatabaseProviderFactory(provider);

            var database = (Database)DatabaseFactory.CreateDatabase("main");
            var connection = database.CreateConnection();
            connection.Open();
            var mapper = MapBuilder<Status>.MapNoProperties()
                .Map(x => x.Id).ToColumn("Id")
                .Map(x => x.Name).ToColumn("Name")
                .Build();

            try
            {
                var stopTime = new System.Diagnostics.Stopwatch();
                stopTime.Start();
                var vurObjects = database.ExecuteSqlStringAccessor<Status>("SELECT * FROM Status", mapper);

                foreach (var item in vurObjects)
                {
                    Debug.WriteLine(item.Id);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
