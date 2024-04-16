using FailBetter.Core.QAssoc;
using HarmonyLib;
using Sunless.Game.DataHelpers;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Journal;
using System.Collections.Generic;
using System.Linq;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(StoriesPanel))]
    public class SS_StoriesPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, new[] { typeof(Gazetteer), typeof(IEnumerable<IGrouping<string, CharacterQPossession>>) })]
        public static IEnumerable<CodeInstruction> SSPatch_StoriesPanel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                QualityHelper.MiscCategoryName = "杂项"; // 虽然不规范，但是能用(暂时)
                SS_Utility.ILReplacer(ref instructions, "Journal", "日志");
            });
            return instructions;
        }
    }
}
