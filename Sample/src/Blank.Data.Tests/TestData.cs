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
            var users = mvModel.GetAllUsers();
            var count = users.Count;

            mvModel.AddNewEntity(new User() { Firstname = "TEST", Lastname = "TEST", Username = "TEST" });

            users = mvModel.GetAllUsers();
            var count1 = users.Count;

            Assert.IsTrue((count+1) == count1);
        }
    }
}
