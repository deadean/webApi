using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common.Interfaces.Factories;

namespace WebApi.Common.Implementations.Factories
{
	public class ObjectsByTypeFactory : IObjectsByTypeFactory
	{
		private static Dictionary<Type, Func<object,object>> modRegisteredObjectsByType = new Dictionary<Type, Func<object,object>>();
		private static readonly IObjectsByTypeFactory modInstance = new ObjectsByTypeFactory();

		private ObjectsByTypeFactory()
		{

		}

		public static IObjectsByTypeFactory GetFactory()
		{
			return modInstance;
		}

		public R GetObjectFromFactory<R>(object obj)
			where R : class
		{
			Func<object, object> func;
			modRegisteredObjectsByType.TryGetValue(obj.GetType().GetInterfaces().LastOrDefault(), out func);

			if (func == null)
				return default(R);

			return func(obj) as R;
		}

		public R GetObjectFromFactory<T,R>()
			where R : class
		{
			Func<object, object> func;
			modRegisteredObjectsByType.TryGetValue(typeof(T), out func);

			if (func == null)
				return default(R);

			return func(null) as R;
		}

		public void RegisterObject<T, R>(Func<T, R> func)
			where R : class
		{
			modRegisteredObjectsByType[typeof(T)] = o => func((T)o);
		}

		public void RegisterObject<T, R>(Func<R> func)
			where R : class
		{
			modRegisteredObjectsByType[typeof(T)] = o => func();
		}
	}
}
