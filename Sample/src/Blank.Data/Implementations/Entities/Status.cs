using Blank.Data.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank.Data.Implementations.Entities
{
    public class Status : IEntity
    {
        #region Properties

        public long Id
        {
            get;
            set;
        }

        public virtual long StatusId { get; set; }
        public virtual string Name { get; set; }
        public virtual int Ordinal { get; set; }
        public virtual byte[] Version { get; set; }

        #endregion

    }
}
