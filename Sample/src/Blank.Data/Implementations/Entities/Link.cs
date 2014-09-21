using Blank.Data.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank.Data.Implementations.Entities
{
    public sealed class Link : IEntity
    {
        #region Properties

        public long Id { get; set; }

        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
        
        #endregion
        
    }
}
