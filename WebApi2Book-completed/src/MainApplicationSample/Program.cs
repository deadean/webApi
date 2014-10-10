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

                    var query1 = session.Query<Task>().ToList();
                    foreach (var item in query1)
                    {
                        Console.WriteLine(item.Subject);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
