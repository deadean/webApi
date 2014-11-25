using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Data.Implementations.Entities;
using WebApi.DataBase.Sqlite.EnterpriseLibrary.ModelServices;

namespace WebApi.Tests.Data.Sqlite.EnterpriseLibrary
{
	[TestClass]
	public class WebApi_Tests_Data_Sqlite_EnterpriseLibrary
	{
		private IModelServices mvModel = ModelServices.GetInstance();

		[TestMethod]
		public void WebApi_Tests_Data_Sqlite_EnterpriseLibrary_Test1()
		{
			var statuses = mvModel.GetEntities<Status>();
			var res = statuses.Count();
			Assert.AreEqual(res,1);
		}
	}
}
