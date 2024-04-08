using HarmonyLib;
using Sunless.Game.Phenomena.AdHoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(ExplosiveMine))]
    public class SS_ExplosiveMine
    {
        [HarmonyTranspiler]
        [HarmonyPatch("GetMessage")]
        public static IEnumerable<CodeInstruction> SSPatch_GetMessage(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Suffered "] = "受到了",
                    [" damage!"] = "点伤害！"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
