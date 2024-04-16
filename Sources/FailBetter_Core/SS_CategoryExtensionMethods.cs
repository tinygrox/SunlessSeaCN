using FailBetter.Core;
using FailBetter.Core.Enums;
using FailBetter.Core.ExtensionMethods.EnumExtensionMethods;
using HarmonyLib;
using System.Collections.Generic;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(CategoryExtensionMethods))]
    public class SS_CategoryExtensionMethods
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CategoryExtensionMethods.DisplayName), [typeof(Category), typeof(World)])]
        public static IEnumerable<CodeInstruction> SSPatch_DisplayName(IEnumerable<CodeInstruction> instructions)
        {
            // 这里的翻译我还没知道是哪里的，如果翻错，那再说。
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Performance"] = "表现", // 暂译
                    ["Personal"] = "个人", // 暂译
                    ["Triangulation"] = "三角测量" // 暂译
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CategoryExtensionMethods.DisplayNameSunless), [typeof(Category)])]
        public static IEnumerable<CodeInstruction> SSPatch_DisplayNameSunless(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Cargo"] = "货物",
                    ["Curiosities"] = "奇珍异闻",
                    ["Stories"] = "轶事",
                    ["Accomplishments"] = "历往",
                    ["Circumstances"] = "形势" // 暂译
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

    }
}
