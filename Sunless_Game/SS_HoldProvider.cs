using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSTranslator
{
    [HarmonyPatch]
    public class SS_HoldProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Sunless.Game.ApplicationProviders.HoldProvider), nameof(HoldProvider.Assay))]
        public static IEnumerable<CodeInstruction> SSPatch_Assay(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["This cannot be sold on the London markets"] = "此物在沦敦市场无法出售",
                ["This would fetch {0} {1} in Fallen London"] = "此物在沦敦中值 {0}{1}"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Sunless.Game.ApplicationProviders.HoldProvider), nameof(HoldProvider.OpenNameDialog))]
        public static IEnumerable<CodeInstruction> SSPatch_OpenNameDialog(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Rename your ship"] = "重命名你的舰船",
                ["Go"] = "继续"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
    }
}
