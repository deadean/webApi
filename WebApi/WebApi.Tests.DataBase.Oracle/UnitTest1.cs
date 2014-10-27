using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.DataBase.Oracle;
using System.Data.Common;

namespace WebApi.Tests.DataBase.Oracle
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestOracleConnection()
        {
            CDataBase.close();
            CDataBase.DataSource = "RONI";
            CDataBase.UserID = "VUDATA";
            CDataBase.Password = "guru";

            CDataBase.connect();

            var classType = Load(null);

            CDataBase.close();

            Assert.IsTrue(classType=="PROJECT");
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
