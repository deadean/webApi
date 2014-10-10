using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi2Book.Data.SqlServer.Mapping;

namespace Blank.ConsoleMain
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)

                    InitializeSessionFactory();
                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            var connectionStr = MsSqlConfiguration.MsSql2008
                  .ConnectionString(@"Data Source=WE-04;Initial Catalog=WebApi2BookDb;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            _sessionFactory = Fluently.Configure()
                .Database(connectionStr)
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                .BuildSessionFactory();
        }

        private static void InitializeAndCreatDbTables()
        {
            var connectionStr = MsSqlConfiguration.MsSql2008
                  .ConnectionString(@"Data Source=WE-04;Initial Catalog=WebApi2BookDb;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False");
            _sessionFactory = Fluently.Configure()
                .Database(connectionStr)
                .Mappings(
                    m =>
                        m.FluentMappings.AddFromAssemblyOf<StatusMap>()
                )
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
