using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DataBase.Oracle.EnterpriseLibrary
{
    public class Connection
    {
        class CVurObject
        {
            public string ID { get; set; }
            public string ClassType { get; set; }
        }

        class CParameterMapper : IParameterMapper
        {
            public Database Database;

            public void AssignParameters(DbCommand command, object[] parameterValues)
            {
                foreach (OracleParameter param in parameterValues)
                {
                    Database.AddInParameter(command, param.ParameterName, param.DbType, param.Value);
                }

                Database.DiscoverParameters(command);
            }
        }

        public void Connect()
        {
            var configuration = new SystemConfigurationSource();
            var provider = new DatabaseProviderFactory(configuration);
            DatabaseFactory.SetDatabaseProviderFactory(provider);

            var database = (Database)DatabaseFactory.CreateDatabase("RONI");
            var connection = database.CreateConnection();
            connection.Open();
            var mapper = MapBuilder<CVurObject>.MapNoProperties()
                .Map(x => x.ID).ToColumn("REC_ID")
                .Map(x => x.ClassType).ToColumn("CLASS_TYPE")
                .Build();

            try
            {
                var stopTime = new System.Diagnostics.Stopwatch();
                stopTime.Start();
                //var vurObjects = database.ExecuteSqlStringAccessor<CVurObject>("SELECT * FROM VUR_OBJECT", mapper);

                //foreach (var item in vurObjects)
                {
                    //Debug.WriteLine(item.ID);
                }

                stopTime.Stop();
                Debug.WriteLine(stopTime.ElapsedMilliseconds);

                var pMapper = new CParameterMapper() { Database = database };

                var vurObjectsByProc = database.ExecuteSprocAccessor<CVurObject>("AD_REC.record_hist_select", pMapper, mapper, new OracleParameter("p_rec_id", "ROOT_PROJECT_000000000"));

                foreach (var item in vurObjectsByProc)
                {
                    Console.WriteLine(item.ID);
                }
            }
            catch (Exception ex)
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
