using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Shipyard;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(ShipShopPanel))]
    public class SS_ShipShopPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Gazetteer), typeof(List<Availability>)])]
        public static IEnumerable<CodeInstruction> SSPatch_ShipShopPanel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Available ships"] = "待售舰船"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }
    }
}
