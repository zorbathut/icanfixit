using Harmony;
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
    [HarmonyPatch(typeof(Building), "Destroy")]
    public static class Building_Patch_Destroy
    {
        public static void Prefix(ref Map __state, Building __instance, DestroyMode mode)
        {
            __state = __instance.Map;
        }

        public static void Postfix(Map __state, Building __instance, DestroyMode mode)
        {
            if (mode == DestroyMode.KillFinalize && __instance.Faction == Faction.OfPlayer && __instance.def != null && __instance.def.blueprintDef != null)
            {
                GenConstruct.PlaceBlueprintForBuild(__instance.def, __instance.Position, __state, __instance.Rotation, Faction.OfPlayer, __instance.Stuff);
            }
        }
    }
}
