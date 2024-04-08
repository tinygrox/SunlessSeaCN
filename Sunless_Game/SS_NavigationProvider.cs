using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using Sunless.Game.Data.SNRepositories;
using Sunless.Game.Dictionaries;
using Sunless.Game.Entities.Geography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(NavigationProvider))]
    public class SS_NavigationProvider
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.AutoAddFuelBarrel))]
        public static IEnumerable<CodeInstruction> SSPatch_AutoAddFuelBarrel(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Our fuel reserves are empty.", "我们的燃油储备已见底。");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.TurboCharge))]
        public static IEnumerable<CodeInstruction> SSPatch_TurboCharge(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["We're out of fuel!"] = "我们的燃油已耗尽！",
                ["FULL POWER! (Beware: your engines will  occasionally explode.)"] = "满功率运行！（小心：你的引擎存在概率会爆炸。）"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.ToggleShipTransform))]
        public static IEnumerable<CodeInstruction> SSPatch_ToggleShipTransform(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["surface"] = "上浮",
                ["dive"] = "下潜",
                ["You can't "] = "你无法在此处进行",
                [" here. Seek deeper waters."] = "。去深一点的水域"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.FeedCrew))]
        public static IEnumerable<CodeInstruction> SSPatch_FeedCrew(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Fed the crew.", "喂养乘员。");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.OfficerReassignmentTerrorBump))]
        public static IEnumerable<CodeInstruction> SSPatch_OfficerReassignmentTerrorBump(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "The reassignment has worried the crew! Change officers in port to avoid this.", "乘员对职务重新分配一事感到担忧！在港口中更换军官避免此影响。");
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.RecoverFromCorruption))]
        public static IEnumerable<CodeInstruction> SSPatch_RecoverFromCorruption(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["An update has moved your port!"] = "你的港口获得了一项升级！",
                ["Sorry about that. Would you like to return to London?"] = "不好意思。你想要返回伦敦吗？"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.Dock), [typeof(PortDatum), typeof(Vector3), typeof(bool), typeof(bool)])]
        // 史上最 SB 的代码，明明有 GetById，非要使用那个逼 GetByName - 尚未测试是否正确生效
        public static IEnumerable<CodeInstruction> SSPatch_Dock(IEnumerable<CodeInstruction> instructions)
        {
            // 顺便的额外工作，之所以插入在这里，是因为这里是游戏一开始的状态，即 DOCK 状态
            if (ContextButtonDictionary.DockButton.Label == "DOCK")
            {
                ContextButtonDictionary.DockButton.Label = "停泊";
            }
            if (ContextButtonDictionary.LaunchButton.Label == "LAUNCH")
            {
                ContextButtonDictionary.LaunchButton.Label = "启程";
            }
            if (ContextButtonDictionary.CombatStart.Label == "BATTLE")
            {
                ContextButtonDictionary.CombatStart.Label = "战斗";
            }
            if (ContextButtonDictionary.CombatEnd.Label == "ALL CLEAR")
            {
                ContextButtonDictionary.CombatEnd.Label = "退出战斗";
            }
            //

            var codes = instructions.ToList();

            // 看不懂的用 dnspy 看 IL Code
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldstr)
                {
                    if (codes[i].operand.ToString() == "Memoirs: Your Past")
                    {
                        codes.RemoveAt(i + 1);
                        codes.RemoveAt(i);
                        codes.Insert(i, new CodeInstruction(OpCodes.Ldc_I4, 108651));
                        codes.Insert(i + 1, new CodeInstruction(OpCodes.Callvirt, typeof(Sunless.Game.Data.SNRepositories.QualityRepository).GetMethod("GetById")));
                    }
                    if (codes[i].operand.ToString() == "Addressed As")
                    {
                        codes.RemoveAt(i + 1);
                        codes.RemoveAt(i);
                        codes.Insert(i, new CodeInstruction(OpCodes.Ldc_I4, 102969));
                        codes.Insert(i + 1, new CodeInstruction(OpCodes.Callvirt, typeof(Sunless.Game.Data.SNRepositories.QualityRepository).GetMethod("GetById")));
                    }
                    if (codes[i].operand.ToString() == "a stranger")
                    {
                        codes.RemoveAt(i + 1);
                        codes.RemoveAt(i);
                        codes.Insert(i, new CodeInstruction(OpCodes.Ldc_I4, 231));
                        codes.Insert(i + 1, new CodeInstruction(OpCodes.Callvirt, typeof(Sunless.Game.Data.SNRepositories.QualityRepository).GetMethod("GetById")));
                    }
                    if (codes[i].operand.ToString() == "Memoirs: Quality of Lodgings")
                    {
                        codes.RemoveAt(i + 1);
                        codes.RemoveAt(i);
                        codes.Insert(i, new CodeInstruction(OpCodes.Ldc_I4, 108290));
                        codes.Insert(i + 1, new CodeInstruction(OpCodes.Callvirt, typeof(Sunless.Game.Data.SNRepositories.QualityRepository).GetMethod("GetById")));
                    }
#if DEBUG
                    // 抓 operand
                    Debug.Log($"[SSTranslator] - [OP-callvirt] {codes[i + 1].operand} - {codes[i].operand}");
                    Debug.Log($"[SSTranslator] - [OP-call] {codes[i - 1].operand}");
#endif
                }
            }
            return codes.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.Undock), [typeof(PortDatum), typeof(Vector3)])]
        public static IEnumerable<CodeInstruction> SSPatch_Undock(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "WHY WON'T IT WORK? ...oh, we didn't install an engine.", "<b>为什么就是不能运行？</b>……噢，我们没有装引擎。");
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(NavigationProvider.ListenKeyboardEvents))]
        public static IEnumerable<CodeInstruction> SSPatch_ListenKeyboardEvents(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["surface"] = "上浮",
                ["dive"] = "下潜",
                ["Cannot "] = "当处于港口时无法",
                [" while at Port"] = "。",
                ["surfacing"] = "上浮操作",
                ["diving"] = "下潜操作",
                ["An obstruction prevents you from "] = "障碍物阻碍了你的",
                ["! Seek clearer waters."] = "！去宽畅一点的水域。",
                ["We're out of fuel!"] = "我们的燃油已耗尽！"
            };
            //trans = trans.Concat(SS_Utility.KeyScheme).ToDictionary(kk => kk.Key, kk => kk.Value);
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
    }
}
