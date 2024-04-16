using FailBetter.Core;
using FailBetter.Core.QAssoc;
using HarmonyLib;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Gazetteer.Officers;
using System.Collections.Generic;
using System.Linq;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(AvailableOfficers))]
    public class SS_AvailableOfficers
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Gazetteer), typeof(IEnumerable<IGrouping<Quality, CharacterQPossession>>)])]
        public static IEnumerable<CodeInstruction> SSPatch_AvailableOfficers(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Available Officers", "可用军官");
            });
            return instructions;
        }
    }
}
