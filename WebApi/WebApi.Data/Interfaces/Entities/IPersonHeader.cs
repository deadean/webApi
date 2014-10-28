using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Data.Interfaces.Entities
{
    public interface IPersonHeader
    {
        string PersonID { get; set; }
        string PreName { get; set; }
        string PostName { get; set; }
        string Function { get; set; }
    }
}
