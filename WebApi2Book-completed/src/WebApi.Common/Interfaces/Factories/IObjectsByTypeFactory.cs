using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Common.Interfaces.Factories
{
	public interface IObjectsByTypeFactory
	{
		R GetObjectFromFactory<R>(object obj) 
			where R : class;
		void RegisterObject<T, R>(Func<T, R> func)
			where R : class;
	}
}
