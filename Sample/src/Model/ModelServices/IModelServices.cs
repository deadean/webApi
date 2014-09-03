using Model.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ModelServices
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
