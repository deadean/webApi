using Blank.Data.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blank.Data.Implementations.Entities
{
    internal class User:IEntity
    {
        #region Fields

        private List<Link> _links = new List<Link>();
        
        #endregion

        #region Properties

        public long ID { get; set; }

        public virtual long UserId { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Username { get; set; }
        public virtual byte[] Version { get; set; }
        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }
        
        #endregion

        #region Public Methods

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        #endregion


    }
}
