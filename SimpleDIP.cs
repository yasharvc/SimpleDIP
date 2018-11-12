using System;
using System.Collections.Generic;

namespace SimpleDIP
{
	public class SimpleDIP
	{
		private static Dictionary<Type, Type> converter = new Dictionary<Type, Type>();

		public static void Register<T>()
		{
			if(!typeof(T).IsAbstract)
				converter[typeof(T)] = typeof(T);
			ResolveAllInterfaces(typeof(T));
			ResolveBaseClass(typeof(T));
		}

		public static void Register(Type t)
		{
			ResolveAllInterfaces(t);
			ResolveBaseClass(t);
		}
		public static void Register<T>(Type t)
		{
			converter[typeof(T)] = t;
			ResolveAllInterfaces(t);
			ResolveBaseClass(t);
		}

		private static void ResolveAllInterfaces(Type t)
		{
			Type[] interfaces = t.GetInterfaces();
			foreach (var _interface in interfaces)
			{
				converter[_interface] = t;
			}
		}

		private static void ResolveBaseClass(Type type)
		{
			var baseType = type.BaseType;
			if(baseType != null && baseType != typeof(object) && !converter.ContainsKey(baseType))
				converter[baseType] = type;
		}

		public static T CreateInstance<T>() where T : class
		{
			try
			{
				return Resolve<T>();
			}
			catch
			{
				Type type = converter[typeof(T)];
				var constructors = type.GetConstructors();
				T res = null;
				foreach (var constructor in constructors)
				{
					var parameters = constructor.GetParameters();
					var pars = new List<Object>();
					foreach (var x in parameters)
					{
						pars.Add(CreateInstance(x.ParameterType));
					}
					res = (T)constructor.Invoke(pars.ToArray());
					break;
				}
				return res;
			}
		}

		private static object CreateInstance(Type t){
			try{
				return Resolve(t);
			}catch{
				var constructors = t.GetConstructors();
				Object res = null;
				foreach (var constructor in constructors)
				{
					var parameters = constructor.GetParameters();
					var pars = new List<Object>();
					foreach (var x in parameters)
					{
						pars.Add(CreateInstance(x.ParameterType));
					}
					res = constructor.Invoke(pars.ToArray());
					break;
				}
				return res;
			}
		}


		public static void Register<T, V>()
		{
			//if(!typeof(T).IsAbstract)
				converter[typeof(T)] = typeof(V);
			//ResolveAllInterfaces(typeof(V));
		}

		public static object Resolve(Type type){
			return Activator.CreateInstance(converter[type]);
		}
		public static T Resolve<T>()
		{
			return (T)Activator.CreateInstance(converter[typeof(T)]);
		}
	}
}