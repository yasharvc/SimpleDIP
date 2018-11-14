using System;
using System.Reflection.Emit;
//http://www.cesarafonso.pt/2013/11/is-activator-that-bad.html
public class DynamicInitializer
{
    private static Func<object> CachedVersion;

    public static object ObjectGenerator(Type type){
        var target = type.GetConstructors()[0];
        var dynamic = new DynamicMethod(string.Empty,type,new Type[0],target.DeclaringType);
        var il = dynamic.GetILGenerator();
        il.DeclareLocal(target.DeclaringType);
        il.Emit(OpCodes.Newobj,target);
        il.Emit(OpCodes.Stloc_0);
        il.Emit(OpCodes.Ldloc_0);
        il.Emit(OpCodes.Ret);

        CachedVersion = (Func<object>)dynamic.CreateDelegate(typeof(Func<object>));
        return CachedVersion();
    }

    public static object NewInstanceCached(){
        return CachedVersion();
    }

    public static T NewInstance<T>() where T:class{
        return ObjectGenerator(typeof(T)) as T;
    }

    public static object NewInstance(Type T){
        var x =  ObjectGenerator(T);
        return x;
    }
}