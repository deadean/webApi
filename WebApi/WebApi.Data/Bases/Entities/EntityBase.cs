using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Data.Entities.Interfaces;

namespace WebApi.Data.Entities.Bases
{
    public class EntityBase : IEntity
    {
        public string ID { get; set; }
    }
}
