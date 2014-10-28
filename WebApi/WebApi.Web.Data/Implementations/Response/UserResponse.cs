using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Web.Data.Interfaces.Response;

namespace WebApi.Web.Data.Implementations.Response
{
    public class UserResponse : IUserResponse
    {
        public string PersonID { get; set; }
        public string PreName { get; set; }
        public string PostName { get; set; }
        public string Function { get; set; }
    }
}
