using HarmonyLib;
using Sunless.Game.ExtensionMethods;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(StringExtensionMethods))]
    public class SS_StringExtensionMethods
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(StringExtensionMethods.Pluralise), new[] { typeof(string) })]
        public static IEnumerable<CodeInstruction> SSPatch_Pluralise(IEnumerable<CodeInstruction> instructions)
        {
            // 考虑到如果使用的完整汉化后不大可能存在最后一个字母为 'o' 的情况，所以原扩展方法中的 es 后缀就不改了，同理，下方的 GetIndefiniteArticle 也没有必要改动 'an'
            SS_Utility.ILReplacer(ref instructions, "s", "");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(StringExtensionMethods.GetIndefiniteArticle), new[] { typeof(string) })]
        public static IEnumerable<CodeInstruction> SSPatch_GetIndefiniteArticle(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "a", "");
            return instructions;
        }
    }
}
