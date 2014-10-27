using System.Data.Common;

namespace WebApi.DataBase.Oracle
{
	public class CDbConnectionProperties
	{
		public string DBConnectionID { get; set; }
		public DbConnection DBConnection { get; set; }
		public string DBConnectionSID { get; set; }
	}
}
