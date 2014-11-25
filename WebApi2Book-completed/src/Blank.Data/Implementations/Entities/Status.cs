using Blank.Data.Interfaces.Entities;

namespace WebApi.Data.Implementations.Entities
{
    public class Status : IEntity, IVersionedEntity
    {
        #region Properties

        public long Id
        {
            get;
            set;
        }

        public virtual string Name { get; set; }
        public virtual int Ordinal { get; set; }
        public virtual string Version { get; set; }

        #endregion

    }
}
