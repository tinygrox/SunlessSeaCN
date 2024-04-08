using FailBetter.Core.QAssoc;
using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using Sunless.Game.Entities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(TradeRouteProvider))]
    public class SS_TradeRouteProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(TradeRouteProvider.GetAcceptableName))]
        public static IEnumerable<CodeInstruction> SSPatch_GetAcceptableName(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldloc_0)
                {
                    CodeInstruction call = CodeInstruction.Call(typeof(SS_Utility), nameof(SS_Utility.GetCNShipNames));
                    codes.Insert(i, call);
                    break;
                }
            }
            return codes.AsEnumerable();
        }

        //[HarmonyPostfix]
        //[HarmonyPatch(nameof(TradeRouteProvider.GetAcceptableName))]
        //static void returnShipName(ref string __result)
        //{
        //    string playerBoatName = GameProvider.Instance.CurrentCharacter.QualitiesPossessedList.Where((CharacterQPossession x) => x.AssociatedQuality == WellKnownQualityProvider.Ship).FirstOrDefault<CharacterQPossession>().EquippedPossession.Name;
        //    List<string> list = SS_Utility.CNShipNames.Where(x => x != playerBoatName).ToList<string>();
        //    __result = list[UnityEngine.Random.Range(0, list.Count<string>())];
        //}
    }
}
