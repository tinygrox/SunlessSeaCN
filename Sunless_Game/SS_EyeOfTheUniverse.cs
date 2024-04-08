using HarmonyLib;
using Sunless.Game.Phenomena.AdHoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(EyeOfTheUniverse))]
    public class SS_EyeOfTheUniverse
    {
        [HarmonyTranspiler]
        [HarmonyPatch("AbyssalGaze")]
        public static IEnumerable<CodeInstruction> SSPatch_AbyssalGaze(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    [" (gained "] = "(增长了",
                    [" Terror)"] = "恐惧)"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
