using System;
using System.Collections.Generic;

namespace Model.Entity
{
    public partial class Company:IEntity
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
