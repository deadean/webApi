using Blank.Data.Interfaces.Entities;
using System.Collections.Generic;

namespace WebApi.DataBase.Sqlite.EnterpriseLibrary.ModelServices
{
    public interface IModelServices
    {
        List<T> GettEntities<T>() where T : IEntity;
    }
}
