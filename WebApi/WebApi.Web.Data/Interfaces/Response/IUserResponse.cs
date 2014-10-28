using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data.Interfaces.Entities;
using WebApi.Web.Common.Interfaces.Response;

namespace WebApi.Web.Data.Interfaces.Response
{
    public interface IUserResponse : IResponse, IPersonHeader
    {
    }
}
