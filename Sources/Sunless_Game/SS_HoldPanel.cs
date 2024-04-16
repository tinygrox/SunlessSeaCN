using FailBetter.Core.QAssoc;
using HarmonyLib;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Hold;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(HoldPanel))]
    public class SS_HoldPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Gazetteer), typeof(List<CharacterQPossession>), typeof(List<CharacterQPossession>)])]
        public static IEnumerable<CodeInstruction> SSPatch_HoldPanel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Your hold", "你的货舱");
            });
            return instructions;
        }
    }
}
