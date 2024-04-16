using HarmonyLib;
using Sunless.Game.Appearance;
using Sunless.Game.UI.HUD;
using System;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(FuelBar))]
    public class SS_FuelBar
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(FuelBar.GetTemperatureTooltip))]
        public static void SSPatch_GetTemperatureTooltip(ref string __result, float ____tempurature)
        {
            __result = string.Concat(new object[]
            {
                TextStyle.B,
                "引擎温度：",
                TextStyle.B_End,
                Convert.ToInt32(____tempurature),
                "°F/",
                Convert.ToInt32((____tempurature-32)*5/9),
                "°C"
            });
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(FuelBar.getFuelTooltip))]
        public static void SSPatch_getFuelTooltip(ref string __result, float ____fuelInbarrel)
        {
            __result = $"燃油桶内剩余燃料 {Convert.ToInt32(____fuelInbarrel / 100f)}%";
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(FuelBar.getFuelBarrelTooltip))]
        public static IEnumerable<CodeInstruction> SSPatch_getFuelBarrelTooltip(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Fuel barrels: ", "燃油桶：");
            });
            return instructions;
        }
    }
}
