using HarmonyLib;
using Sunless.Game.Formatters;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(LogEntries))]
    public class SS_LogEntries
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(LogEntries.GetInGameTime))]
        public static IEnumerable<CodeInstruction> SSPatch_GetInGameTime(IEnumerable<CodeInstruction> instructions)
        {
            // 将原来的 {月}0 {日}1{英文后缀}2 {年}0 的日期格式改为 {年} {月} {日}{无后缀}
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    //["st"] = "",
                    //["nd"] = "",
                    //["rd"] = "",
                    //["th"] = "",
                    ["{0:MMMM} {1}{2}, {0:yyyy}"] = "{0:yyyy} {0:MM}月 {1}"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
