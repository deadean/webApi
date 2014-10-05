using Blank.Data.Interfaces.Entities;

namespace Blank.Data.Implementations.Entities
{
    public sealed class Link : IEntity, IVersionedEntity
    {
        #region Properties

        public long Id { get; set; }

        public string Rel { get; set; }
        public string Href { get; set; }
        public string Method { get; set; }
        
        #endregion

        public string Version { get; set; }
    }
}
