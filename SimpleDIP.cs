using System;
using System.Collections.Generic;

namespace SimpleDIP
{
    public class SimpleDIP
    {
        private static Dictionary<Type, Type> converter = new Dictionary<Type, Type>();

        public static void Register<T>()
        {
            ResolveAllInterfaces(typeof(T));
        }
        public static void Register(Type t)
        {
            ResolveAllInterfaces(t);
        }
        public static void Register<T>(Type t)
        {
            converter[typeof(T)] = t;
            ResolveAllInterfaces(t);
        }

        private static void ResolveAllInterfaces(Type t)
        {
            Type[] interfaces = t.GetInterfaces();
            foreach (var _interface in interfaces)
            {
                converter[_interface] = t;
            }
        }

        public static void Register<T, V>()
        {
            converter[typeof(T)] = typeof(V);
            ResolveAllInterfaces(typeof(V));
        }
        public static T Resolve<T>()
        {
            return (T)Activator.CreateInstance(converter[typeof(T)]);
        }
    }
}