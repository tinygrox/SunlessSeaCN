using FailBetter.Core.QAssoc;
using HarmonyLib;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Gazetteer.Officers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(CurrentOfficers))]
    public class SS_CurrentOfficers
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Gazetteer), typeof(List<CharacterQPossession>)])]
        public static IEnumerable<CodeInstruction> SSPatch_CurrentOfficers(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Current Officers", "当前军官");
            });
            return instructions;
        }
    }
}
