using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Web.Data.Bases;

namespace WebApi.Web.Data.Implementations
{
    public class EntityBaseImplementation:EntityBase
    {
        #region Fields
        #endregion

        #region Ctor

        public EntityBaseImplementation()
        {
            ID = DateTime.Now.ToString();
        }

        #endregion

        #region Private Methods
        #endregion

        #region Public Methods
        #endregion

        #region Protected Methods
        #endregion

        #region Properties
        #endregion
    }
}
