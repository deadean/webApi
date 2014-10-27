using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using WebApi.DataBase.Oracle;
using WebApi.DataBase.Oracle.EnterpriseLibrary;

namespace WebApi.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
                //CDataBase.close();
                //CDataBase.DataSource = "RONI_DE";
                //CDataBase.UserID = "VUDATA";
                //CDataBase.Password = "guru";

                //CDataBase.connect();

                //var classType = Load(null);
                //System.Console.WriteLine(classType);

                //CDataBase.close();

                Connection cn = new Connection();
                cn.Connect();

			}
			catch(Exception ex)
			{
				System.Console.WriteLine(ex);
			}
		}

        public static CDBQuery sqlRecordObjectRead(string recordID)
        {
            CDBQuery parameters = new CDBQuery("AD_REC.record_hist_select");
            parameters.Add("p_rec_id", recordID);
            return parameters;
        }

        public static string Load(CTransaction transaction)
        {
            var result = string.Empty;
            try
            {
                CDBQuery dbQuery = sqlRecordObjectRead("0710310932170041472853");
                using (DbDataReader reader = CDataBase.getReader(dbQuery, transaction))
                {
                    if (reader != null && reader.HasRows(dbQuery))
                    {
                        while (reader.Read())
                        {
                            result = reader.GetValue(1).ToString();
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                result = string.Empty;
                CLog.Log("Load()", exc);
            }

            return result;
        }
	}
}
