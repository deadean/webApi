using Blank.Data.Interfaces.Entities;
using System;
using System.Collections.Generic;

namespace Blank.Data.Implementations.Entities
{
    public class Task : IEntity, IVersionedEntity
    {
        #region Fields

        private List<User> _users = new List<User>();
        private List<Link> _links = new List<Link>();
        
        #endregion

        #region Properties

        public long Id { get; set; }
        public long? TaskId { get; set; }
        public string Subject { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public Status Status { get; set; }
        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }
        
        public List<User> Assignees { get; set; }

        public virtual IList<User> Users
        {
            get { return _users; }
        }
        public virtual string Version { get; set; }
        
        #endregion

        #region Public Methods

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        #endregion


    }
}
