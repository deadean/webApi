using System;
using System.Collections.Generic;

namespace Model.Entity
{
    public partial class Category:IEntity
    {
        public Category()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
