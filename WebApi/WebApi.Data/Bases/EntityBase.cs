using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data.Interfaces;

namespace WebApi.Data.Bases
{
    public class EntityBase : IEntity
    {
        public string ID { get; set; }
    }
}
