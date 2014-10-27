using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Web.Data.Interfaces;

namespace WebApi.Web.Data.Bases
{
    public abstract class EntityBase : IEntity
    {
        public string ID { get; set; }
    }
}
