using Blank.Data.Implementations.Entities;
using Blank.Data.SQLite.ModelServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Blank.Data.Tests
{
    [TestClass]
    public class TestData
    {
        private ModelServices mvModel = new ModelServices();

        public void Run()
        {
            TestData_Test1();
        }

        [TestMethod]
        public void TestData_Test1()
        {
            var users = mvModel.GettEntities<User>();
            var count = users.Count;

            mvModel.AddNewEntity(new User() { Firstname = "TEST", Lastname = "TEST", Username = "TEST" });

            users = mvModel.GettEntities<User>();
            var count1 = users.Count;

            Assert.IsTrue((count+1) == count1);
        }

        [TestMethod]
        public void TestData_Test2()
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
