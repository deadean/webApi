using Blank.Data.Interfaces.Entities;
using System.Collections.Generic;

namespace Blank.Data.Implementations.Entities
{
    public class User : IEntity, IVersionedEntity
    {
        #region Fields

        private List<Link> _links = new List<Link>();
        
        #endregion

        #region Properties

        public long Id { get; set; }

        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string Username { get; set; }
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

        public string Version { get; set; }
    }
}
