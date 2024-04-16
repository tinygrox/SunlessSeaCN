using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Shipyard;
using System.Collections.Generic;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(ShipPanel))]
    public class SS_ShipPanel
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Gazetteer), typeof(Quality), typeof(Availability)])]
        public static IEnumerable<CodeInstruction> SSPatch_ShipPanel1(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Your current ship"] = "当前舰船"
                };
                SS_Utility.ILReplacer(instructions, trans, true);
            });
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(Availability)])]
        public static IEnumerable<CodeInstruction> SSPatch_ShipPanel2(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Cost:"] = "售价：",
                    ["Trade:"] = "抵换：",
                    ["Total:"] = "总价："
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(Availability)])]
        static void SSPatch_ShipPanel(ref GameObject ____pricePanel)
        {
            if (____pricePanel != null)
            {
                //Debug.Log($"ShipPanel parent not null and its name = {parent.name}"); // ShipShopPanel(Clone)
                //Debug.Log($"_pricePanel not null and its name = {____pricePanel.name}"); // PriceTable
                SS_Utility.UpdateGameObjectText(____pricePanel, "ButtonBar/Button/Text", "TRADE", "交易");
            }
        }
        [HarmonyTranspiler]
        [HarmonyPatch("StatTableHidden")]
        public static IEnumerable<CodeInstruction> SSPatch_StatTableHidden(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Show Detailed Information"] = "显示详细信息"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(ShipPanel.SetupStatHeader))]
        public static IEnumerable<CodeInstruction> SSPatch_SetupStatHeader(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Detailed Information"] = "详细信息"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("StatTableShown")]
        public static IEnumerable<CodeInstruction> SSPatch_StatTableShown(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Hide Detailed Information"] = "隐藏详细信息"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(ShipPanel.PopulateStatFields))]
        public static IEnumerable<CodeInstruction> SSPatch_PopulateStatFields(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Hull"] = "船体值",
                    ["Hold Capacity"] = "货舱容量",
                    ["Weight"] = "重量",
                    ["Quarters"] = "宿舍",
                    ["Iron bonus"] = "钢铁值增益", // 翻译待改进
                    ["Mirrors bonus"] = "镜子值增益", // 翻译待改进
                    ["Veils bonus"] = "面纱值增益", // 翻译待改进
                    ["Pages bonus"] = "书页值增益", // 翻译待改进
                    ["Hearts bonus"] = "红心值增益", // 翻译待改进
                    ["Ship slots:"] = "舰船槽：",
                    ["Deck"] = "甲板",
                    ["Forward"] = "船艏",
                    ["Auxiliary"] = "辅舱",
                    ["Bridge"] = "驾驶舱",
                    ["Aft"] = "船艉",
                    ["Engines"] = "引擎"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }
    }
}
