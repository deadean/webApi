using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class User : IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
