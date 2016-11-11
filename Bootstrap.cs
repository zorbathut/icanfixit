using CommunityCoreLibrary_ICanFixIt;
using System;
using System.Reflection;
using UnityEngine;
using Verse;

namespace ICanFixIt
{
    class Bootstrap : Def
    {
        public string ModName;

        static Bootstrap()
        {
            try
            {
                MethodInfo method1 = typeof(Verse.Building).GetMethod("Destroy", BindingFlags.Instance | BindingFlags.Public);
                MethodInfo method2 = typeof(Building_Detour).GetMethod("Destroy", BindingFlags.Static | BindingFlags.Public);
                if (!Detours.TryDetourFromTo(method1, method2))
                {
                    Log.Error("EVERYTHING IS BROKEN");
                    return;
                }
            }
            catch (Exception)
            {
                Log.Error("something is seriously wrong");
            }
        }
    }
}
