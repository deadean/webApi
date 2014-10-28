using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebApi.DataBase.Oracle;
using WebApi.Web.Common.Implementations.Controllers;
using WebApi.Web.Common.Interfaces.Response;
using WebApi.Web.Data.Implementations.Response;
using WebApi.WebApiService.Interfaces.DependencyBlock;

namespace WebApi.WebApiService.Controllers
{
    public class UsersController : BaseController
    {
        #region Fields
        #endregion

        #region Ctor

        public UsersController(IUsersDependencyBlock dependencyBlock) : base(dependencyBlock)
        {

        }

        #endregion

        #region Private Methods
        #endregion

        #region Public Get Methods

        //// GET: api/Users
        //[ResponseType(typeof(UserResponse))]
        //public async Task<IHttpActionResult> Get()
        //{
        //    var userResponse = await new UserResponse() { PersonID = "1", PreName = "dean" };
        //    return Ok(userResponse);
        //}

        // GET: api/Users
        public string Get()
        {
            CDataBase.close();
            CDataBase.DataSource = "RONI";
            CDataBase.UserID = "VUDATA";
            CDataBase.Password = "guru";

            CDataBase.connect();

            var classType = Load(null);

            CDataBase.close();

            return classType;
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

        // GET: api/Users/5
        public string Get(int id)
        {
            return "value";
        }

        #endregion

        #region Public Post Methods

        // POST: api/Users
        public void Post([FromBody]string value)
        {
        }

        #endregion

        #region Public Put Methods

        // PUT: api/Users/5
        public void Put(int id, [FromBody]string value)
        {
        }

        #endregion

        #region Public Delete Methods

        // DELETE: api/Users/5
        public void Delete(int id)
        {
        }

        #endregion

        #region Protected Methods
        #endregion

        #region Properties
        #endregion

    }
}
