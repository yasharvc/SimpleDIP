using System;
using System.Collections.Generic;
using System.Reflection;

namespace SimpleDIP
{
    public class SimpleDIP
    {
        private static Dictionary<Type, Type> converter = new Dictionary<Type, Type>();

        public static void Register<T>()
        {
            if (!typeof(T).IsAbstract)
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
            if (baseType != null && baseType != typeof(object) && !converter.ContainsKey(baseType))
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
                return (T)CallConstructor(converter[typeof(T)]);
            }
        }
        private static object CreateInstance(Type t)
        {
            try
            {
                return Resolve(t);
            }
            catch
            {
                return CallConstructor(converter[t]);
            }
        }

        private static object CallConstructor(Type type)
        {
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                var parameters = constructor.GetParameters();
                var pars = new List<Object>();
                foreach (var x in parameters)
                {
                    pars.Add(CreateInstance(x.ParameterType));
                }
                return constructor.Invoke(pars.ToArray());
            }
            throw new Exception();
        }



        public static void Register<T, V>()
        {
            converter[typeof(T)] = typeof(V);
        }

        public static object Resolve(Type type)
        {
            //var x =  Activator.CreateInstance(converter[type]);
			var x = DynamicInitializer.NewInstance(converter[type]);
			return x;
        }
        public static T Resolve<T>()
        {
            //return (T)Activator.CreateInstance(converter[typeof(T)]);
            return (T)DynamicInitializer.NewInstance(converter[typeof(T)]);
        }
    }
}