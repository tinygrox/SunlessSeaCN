using HarmonyLib;
using Sunless.Game.Data;
using Sunless.Game.Entities;
using Sunless.Game.Entities.Geography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(CharacterRepository))]
    public class SS_CharacterRepository
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CharacterRepository.LoadGame), new[] { typeof(string) })]
        public static IEnumerable<CodeInstruction> SSPatch_LoadGame(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Restoring fallback save file"] = "正在恢复备用存档文件",
                ["The save file \""] = "存档文件\"",
                [".json\" appears to be corrupt or invalid, however the fallback save file was OK and has been restored."] = ".json\"好像已经损坏或不可用，还好备用存档文件没事现在已经成功恢复。"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CharacterRepository.Save), new[] { typeof(string), typeof(SunlessCharacter) })]
        public static IEnumerable<CodeInstruction> SSPatch_Save(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Backup failed! Please contact support."] = "备份存档失效！请联系支持人员。",
                ["Save failed!"] = "存档失效！"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CharacterRepository.Create), new[] { typeof(string), typeof(SerializableTileConfig) })]
        public static IEnumerable<CodeInstruction> SSPatch_Create(IEnumerable<CodeInstruction> instructions)
        {
            try
            {
                int startIndex = -1;
                int endIndex = -1;

                var codes = instructions.ToList();

                for (int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == OpCodes.Ldelem_Ref)
                    {
                        endIndex = i + 1; // 加一行到那个 callvirt
                        //Debug.LogWarning($"[SSPatch_Create] for i Func {codes[i].opcode} {codes[i].operand}");

                        for (int j = i - 1; j >= 0; j--)
                        {
                            //Debug.LogWarning($"[SSPatch_Create] for j Func {codes[j].opcode} {codes[j].operand}");
                            if (codes[j].opcode == OpCodes.Callvirt &&
                                codes[j].operand.ToString().Equals("FailBetter.Core.QAssoc.CharacterQPossession get_EquippedPossession()"))
                            {
                                //Debug.LogWarning($"[SSPatch_Create] startIndex get: {codes[j].opcode} {codes[j].operand}");
                                startIndex = j; // 提前一行把 ldloc.1包含进去
                                break;
                            }
                        }

                        break;
                        //Debug.LogWarning($"[SSPatch_Create] IL{i} operand String: {codes[i].operand}");
                    }
                }

                if (startIndex > -1 && endIndex > -1)
                {
                    CodeInstruction call = CodeInstruction.Call(typeof(SS_Utility), nameof(SS_Utility.GetCNShipNames)); // 狠狠的插进去！
                    codes.Insert(startIndex, call); // 在 ldloc.1前面插入，其实在哪里插都无所谓，这里只是锻炼一下代码能力
                    //Debug.LogWarning($"[SSPatch_Create] call Func {call.opcode} {call.operand}");
                }
                return codes.AsEnumerable();
            }
            catch (Exception e)
            {
                throw new Exception("[SSTranslator]有地方没能汉化到，出错位置 SSPatch_Create() 出错信息为：", e);
            }
        }
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(CharacterRepository.CreateGenericCharacter), new[] { typeof(string), typeof(SerializableTileConfig) })]
        public static IEnumerable<CodeInstruction> SSPatch_CreateGenericCharacter(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();

            for (int i = 0; i < codes.Count; i++)
            {
                if (i > 0 && codes[i].opcode == OpCodes.Newobj && codes[i].operand.ToString() == "Void .ctor()" && codes[i + 1].opcode == OpCodes.Dup)
                {
                    //Debug.LogWarning($"[SSTranslator]Func <CreateGenericCharacter> {codes[i].opcode} {codes[i].operand}");
                    //Debug.LogWarning($"[SSTranslator]Func <CreateGenericCharacter>+1 {codes[i + 1].opcode} {codes[i + 1].operand}");
                    CodeInstruction call = CodeInstruction.Call(typeof(SS_Utility), nameof(SS_Utility.GetCNShipNames));
                    codes.Insert(i - 1, call);
                    //Debug.LogWarning($"[SSTranslator]Func <CreateGenericCharacter> AFTER {codes[i].opcode} {codes[i].operand}");
                    //Debug.LogWarning($"[SSTranslator]Func <CreateGenericCharacter>+1 AFTER {codes[i + 1].opcode} {codes[i + 1].operand}");
                    break;
                }
            }

            return codes.AsEnumerable();
        }
    }
}
