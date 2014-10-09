using NHibernate;
using NHibernate.Cfg;
using System;
using System.Reflection;
using WebApi2Book.Data.Entities;
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
                using (ISession session = OpenSession())
                {
                    var res = session.QueryOver<Task>();
                    //AllTasksQueryProcessor test = new AllTasksQueryProcessor(session);
                    //test.GetTasks()
                    //using (ITransaction transaction = session.BeginTransaction())
                    //{
                    //    session.Save(rosey);
                    //    transaction.Commit();
                    //}
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
            if (SessionFactory == null) //not threadsafe
            { //SessionFactories are expensive, create only once
                Configuration configuration = new Configuration();
                configuration.AddAssembly(Assembly.GetCallingAssembly());
                SessionFactory = configuration.BuildSessionFactory();
            }
            return SessionFactory.OpenSession();
        }
    }
}
