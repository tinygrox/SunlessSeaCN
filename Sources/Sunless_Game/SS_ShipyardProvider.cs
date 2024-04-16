using FailBetter.Core;
using FailBetter.Core.QAssoc;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Sunless.Game.ApplicationProviders.ShipyardProvider))]
    public class SS_ShipyardProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch("DisplayShipyard")]
        public static IEnumerable<CodeInstruction> SSPatch_DisplayShipyard(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["No Shipyard available"] = "无可用船坞",
                    ["Few ports in the Unterzee sell ships. Look elsewhere."] = "海泽只有少数几个港口出售船只。去别的地方找找。"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("OpenNameDialog", new[] { typeof(CharacterQPossession) })]
        public static IEnumerable<CodeInstruction> SSPatch_OpenNameDialog(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Give your new ship a name"] = "给你的新舰船起个名字",
                    ["Go"] = "继续"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch("SwapShip", new[] { typeof(Quality), typeof(Action), typeof(bool) })]
        public static IEnumerable<CodeInstruction> SSPatch_SwapShip(IEnumerable<CodeInstruction> instructions)
        {
            try
            {
                var codes = instructions.ToList();
                int startIndex = -1;
                for (int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == OpCodes.Stfld)
                    {
                        startIndex = i;
                        break;
                    }
                }

                if (startIndex > -1)
                {
                    CodeInstruction call = CodeInstruction.Call(typeof(SS_Utility), nameof(SS_Utility.GetCNShipNames));
                    codes.Insert(startIndex, call);
                }

                return codes.AsEnumerable();
            }
            catch (Exception ex)
            {
                throw new Exception($"[SSTranslator] - {ex.Message}");
            }
        }

        //[HarmonyPostfix]
        //[HarmonyPatch("OpenNameDialog", new[] { typeof(CharacterQPossession) })]
        //static void SSPatch_RandomShipNames(ref UserInput ____userInput)
        //{
        //    ____userInput.InputText = SS_Utility.CNShipNames[UnityEngine.Random.Range(0, SS_Utility.CNShipNames.Count<string>())];
        //}

    }
}
