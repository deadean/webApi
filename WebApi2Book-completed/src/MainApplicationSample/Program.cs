
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Reflection;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.SqlServer.Mapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;



namespace Blank.ConsoleMain
{
    class Program
    {
        static ISessionFactory SessionFactory;
        static void Main(string[] args)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    var query = session.Query<Status>().ToList();
                    foreach (var item in query)
                    {
                        Console.WriteLine(item.Name);
                    }
                }
                

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        static ISession OpenSession()
        {
            //ISessionFactory _sessionFactory1 = new Configuration()
            //                  .Configure()
            //                  .SetProperty("connection.connection_string", "WebApi2BookDb").BuildSessionFactory();
            ////var connectionString = MsSqlConfiguration.MsSql2008.ConnectionString(
            ////            c => c.FromAppSetting("WebApi2BookDb"));
            ////connectionString.ConnectionString = "Data Source=WE-04;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";
            //var sessionFactory = Fluently.Configure()
            //    .Database(connectionString)
            //    .CurrentSessionContext("web")
            //    .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
            //    .BuildSessionFactory();

            //var session = sessionFactory.OpenSession();
            //return sessionFactory.GetCurrentSession();
            return NHibernateHelper.OpenSession();
        }
    }
}
