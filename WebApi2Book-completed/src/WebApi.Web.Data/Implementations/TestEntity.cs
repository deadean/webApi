using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Web.Data.Bases;

namespace WebApi.Web.Data.Implementations
{
    public class TestEntity : EntityBaseImplementation
    {
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        #region Override Methods

        public override string ToString()
        {
            return string.Format("Type :{0}, ID :{1}, Name :{2}", this.GetType(), this.ID, this.Name);
        }

        #endregion
    }
}
