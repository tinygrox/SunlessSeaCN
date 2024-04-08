using HarmonyLib;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Storylet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(StoryletIn))]
    public class SS_StoryletIn
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Gazetteer), typeof(FailBetter.Core.Event)])]
        public static IEnumerable<CodeInstruction> SSPatch_classname(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Perhaps Not", "还是算了");
            });
            return instructions;
        }
    }
}
