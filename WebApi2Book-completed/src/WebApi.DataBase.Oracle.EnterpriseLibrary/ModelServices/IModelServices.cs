using System.Collections.Generic;

namespace WebApi.DataBase.Sqlite.EnterpriseLibrary.ModelServices
{
    public interface IModelServices
    {
        IEnumerable<T> GetEntities<T>() where T : class, new();
    }
}
