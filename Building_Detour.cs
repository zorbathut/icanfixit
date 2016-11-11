using RimWorld;
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace ICanFixIt
{
    public static class Building_Detour
    {
        public static void Destroy(this Building building, DestroyMode mode = DestroyMode.Vanish)
        {
            // We need to call ThingWithComps.Destroy() directly, which is really high on the list of things you're never supposed to be able to do
            // but, I mean
            // since when have I let that stop me
            MethodInfo method = typeof(ThingWithComps).GetMethod("Destroy", BindingFlags.Public | BindingFlags.Instance);
            IntPtr fptr = method.MethodHandle.GetFunctionPointer();
            ((Action<DestroyMode>)Activator.CreateInstance(typeof(Action<DestroyMode>), building, fptr))(mode);

            InstallBlueprintUtility.CancelBlueprintsFor(building);
            if (mode == DestroyMode.Deconstruct)
            {
                SoundDef.Named("BuildingDeconstructed").PlayOneShot(building.Position);
            }

            // Okay!
            if (mode == DestroyMode.Kill && building.Faction == Faction.OfPlayer)
            {
                GenConstruct.PlaceBlueprintForBuild(building.def, building.Position, building.Rotation, Faction.OfPlayer, building.Stuff);
            }
        }
    }
}
