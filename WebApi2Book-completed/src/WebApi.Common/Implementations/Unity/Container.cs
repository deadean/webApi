using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Common.Enumerations;

namespace WebApi.Common.Implementations.Unity
{
	public class Container
	{
		private static readonly IUnityContainer modContainer;

		static Container()
		{
			modContainer = new UnityContainer();
		}

		public static void RegisterType(Type from, Type to)
		{
			RegisterType(from, to, enTypeLifeTime.Transient);
		}

		public static void RegisterType<TFrom, TTo>()
		{
			RegisterType(typeof(TFrom), typeof(TTo), enTypeLifeTime.Transient);
		}

		public static void RegisterType(Type from, Type to, enTypeLifeTime typeLifeTime)
		{
			modContainer.RegisterType(from, to, GetLifeTimeManagerFromEnum(typeLifeTime));
		}

		public static void RegisterType(Type from, Type to, enTypeLifeTime typeLifeTime, bool fl)
		{
			modContainer.RegisterType(from, to, GetLifeTimeManagerFromEnum(typeLifeTime), new InjectionConstructor());
		}


		public static void RegisterInstance<TInstanceType>(TInstanceType instance)
		{
			modContainer.RegisterInstance(typeof(TInstanceType), instance);
		}

		public static void RegisterInstance(System.Type from, object to)
		{
			modContainer.RegisterInstance(from, to);
		}

		[DebuggerStepThrough]
		public static T Resolve<T>()
		{
			return modContainer.Resolve<T>();
		}

		[DebuggerStepThrough]
		public static T TryResolve<T>()
		{
			return modContainer.IsRegistered<T>()
				? modContainer.Resolve<T>()
				: default(T);
		}

		static LifetimeManager GetLifeTimeManagerFromEnum(enTypeLifeTime typeLifeTime)
		{
			switch (typeLifeTime)
			{
				case enTypeLifeTime.Transient:
					return new TransientLifetimeManager();
				case enTypeLifeTime.Singleton:
					return new ContainerControlledLifetimeManager();
				case enTypeLifeTime.PerThread:
					return new PerThreadLifetimeManager();
				default:
					throw new ArgumentOutOfRangeException("typeLifeTime");
			}
		}
	}
}
