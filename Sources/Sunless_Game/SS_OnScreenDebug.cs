using HarmonyLib;
using Sunless.Game.MonoBehaviours.Editor_Scripts;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(OnScreenDebug))]
    public class SS_OnScreenDebug
    {
        [HarmonyTranspiler]
        [HarmonyPatch("OnGUI")]
        public static IEnumerable<CodeInstruction> SSPatch_OnScreenDebugOnGUI(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Potential Targets: "] = "潜在目标：",
                    ["Null EntityName"] = "空 EntityName"
                };
            });
            return instructions;
        }
    }
}
