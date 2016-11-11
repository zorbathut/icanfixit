using System;
using System.Reflection;
using UnityEngine;
using Verse;

namespace ICanFixIt
{
    static class Util
    {
        public static T GetFieldViaReflection<T>(this object obj, string name) where T : class
        {
            FieldInfo finfo = obj.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);
            if (finfo == null)
            {
                Log.Error(string.Format("Failed to find field {0} in type {1}", name, obj.GetType()));
                return default(T);
            }

            return finfo.GetValue(obj) as T;
        }

        public static void CallMethodViaReflection(this object obj, string name, params object[] param)
        {
            obj.GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj, param);
        }

        public static T CallStaticMethodViaReflection<T>(Type type, string name, params object[] param)
        {
            return (T)type.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, param);
        }
    }
}
