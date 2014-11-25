using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebApi.Tests.Data.Sqlite.EnterpriseLibrary
{
	[TestClass]
	public class WebApi_Tests_Data_Sqlite_EnterpriseLibrary
	{
		[TestMethod]
		public void WebApi_Tests_Data_Sqlite_EnterpriseLibrary_TestMethod1()
		{
			WebApi.DataBase.Sqlite.EnterpriseLibrary.Connection cn = new DataBase.Sqlite.EnterpriseLibrary.Connection();
			cn.Connect();
		}
	}
}
