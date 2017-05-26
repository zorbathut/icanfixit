using Harmony;
using System.Reflection;
using Verse;

namespace ICanFixIt
{
    class ICanFixIt : Mod
    {
        public ICanFixIt(ModContentPack mcp) : base(mcp)
        {
            HarmonyInstance.Create("com.mandible.rimworld.icanfixit").PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
