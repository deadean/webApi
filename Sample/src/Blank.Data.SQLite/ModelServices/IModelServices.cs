using System.Collections.Generic;
using Blank.Data.Implementations.Entities;
using Blank.Data.Interfaces.Entities;

namespace Blank.Data.SQLite.ModelServices
{
    public interface IModelServices
    {
        List<User> GetAllUsers();

        void AddNewEntity(IEntity entity);
        void UpdateEntity(IEntity entity);
        void RemoveEntity(IEntity entity);

        void SaveChanges();
    }
}
