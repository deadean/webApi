using System.Collections.Generic;
using Blank.Data.Implementations.Entities;
using Blank.Data.Interfaces.Entities;

namespace Blank.Data.SQLite.ModelServices
{
    public interface IModelServices
    {
        List<T> GettEntities<T>() where T : IEntity;

        void AddNewEntity(IEntity entity);
        void UpdateEntity(IEntity entity);
        bool RemoveEntity(IEntity entity);

        void SaveChanges();
    }
}
