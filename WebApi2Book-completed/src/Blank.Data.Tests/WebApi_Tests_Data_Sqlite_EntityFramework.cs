using Blank.Data.Implementations.Entities;
using Blank.Data.SQLite.ModelServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Data.Implementations.Entities;

namespace WebApi.Tests.Sqlite.EntityFramework
{
    [TestClass]
    public class WebApi_Tests_Data_Sqlite_EntityFramework
    {
        private ModelServices mvModel = new ModelServices();

        [TestMethod]
				public void WebApi_Tests_Data_Sqlite_EntityFramework_Test1()
        {
            var users = mvModel.GettEntities<User>();
            var count = users.Count;

            mvModel.AddNewEntity(new User() { Firstname = "TEST", Lastname = "TEST", Username = "TEST" });

            users = mvModel.GettEntities<User>();
            var count1 = users.Count;

            Assert.IsTrue((count+1) == count1);
        }

        [TestMethod]
				public void WebApi_Tests_Data_Sqlite_EntityFramework_Test2()
        {
            var statuses = mvModel.GettEntities<Status>();
            var count = statuses.Count;

            mvModel.AddNewEntity(new Status() {Name = "StatusTEST", Ordinal = 1});

            var statuses1 = mvModel.GettEntities<Status>();
            var count1 = statuses1.Count;

            Assert.IsTrue((count + 1) == count1);
        }
    }
}
