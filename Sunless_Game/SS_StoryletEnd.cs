using FailBetter.Core.Result;
using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using Sunless.Game.Entities;
using Sunless.Game.Enums;
using Sunless.Game.UI.Gazetteer;
using Sunless.Game.UI.Storylet;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(StoryletEnd))]
    public class SS_StoryletEnd
    {
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, [typeof(Gazetteer), typeof(StoryletProvider.RunningStory), typeof(IEnumerable<ICoreMessage>), typeof(SunlessCharacter)])]
        public static IEnumerable<CodeInstruction> SSPatch_StoryletEnd(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.ILReplacer(ref instructions, "Continue...", "继续…");
            var codes = instructions.ToList();
            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Stfld && codes[i].operand.ToString() == "Sunless.Game.UI.Gazetteer.Gazetteer _gaz")
                {
                    for (int j = i - 1; j > 0; j--)
                    {
                        if (codes[j].opcode == OpCodes.Ldarg_0)
                        {
                            //Debug.LogWarning($"[SSTranslator] Func <SSPatch_StoryletEnd> {codes[j].opcode} {codes[j].operand}");
                            CodeInstruction call = CodeInstruction.Call(typeof(SS_StoryletEnd), nameof(SS_StoryletEnd.ExoticEffects_Translate));
                            codes.Insert(j, call);
                            break;
                        }
                    }
                    break;
                }
            }
            return codes.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch("CreateContinueButton")]
        public static IEnumerable<CodeInstruction> SSPatch_CreateContinueButton(IEnumerable<CodeInstruction> instructions)
        {
            Dictionary<string, string> Trans = new()
            {
                {"Continue...", "继续…" },
                {"Close Gazetteer", "关闭日志" }
            };
            //SS_Utility.ILReplacer(ref instructions, "Continue...", "继续…");
            //SS_Utility.ILReplacer(ref instructions, "Close Gazetteer", "关闭日志");
            SS_Utility.ILReplacer(instructions, Trans);
            return instructions;
        }

        static void ExoticEffects_Translate()
        {
            foreach (var EE in ExoticEffectFunctions.ExoticEffects)
            {
                switch (EE.Description)
                {
                    case "Choose a Legacy, to preserve something for your next captain.":
                        EE.Description = "选择一项遗留之物，留些东西给你的下一任船长。";
                        break;
                    case "Who are you?":
                        EE.Description = "你为何人？";
                        break;
                    case "Something, somewhere, has changed...":
                        EE.Description = "某事、某地，已经改变了……";
                        break;
                    case "You are returning to port...":
                        EE.Description = "你回到了港口……";
                        break;
                    case "You have a new ship.":
                        EE.Description = "你拥有了艘新船。";
                        break;
                }
            }
        }
    }
}
