using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.Data.SNRepositories;
using Sunless.Game.Phenomena.QualityEffects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(QEffectOverTime))]
    public class SS_QEffectOverTime
    {
        [HarmonyTranspiler]
        [HarmonyPatch("OnTriggerStay2D", new[] { typeof(Collider2D) })]
        public static IEnumerable<CodeInstruction> SSPatch_OnTriggerStay2D(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                var trans = new Dictionary<string, string>()
                {
                    ["Suffered "] = "受到了",
                    [" damage!"] = "点伤害！"
                };
                SS_Utility.ILReplacer(instructions, trans);
            });
            return instructions;
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(QEffectOverTime.TheQuality), MethodType.Getter)]
        public static bool SSPatch_fuckGetByName(ref Quality __result, string ___QualityName)
        {
            //if (___QualityName == "Hull")
            if (QualityRepository.Instance.GetByName(___QualityName) == null)
            {
                Debug.Log($"[QEffectOverTime] -TheQuality 首先 {___QualityName} 是 null 的");
                if (SS_Utility.Name2Id.ContainsKey(___QualityName))
                {

                    Debug.Log($"[QEffectOverTime] 字典里有 {___QualityName}！");
                    var q = QualityRepository.Instance.GetById(SS_Utility.Name2Id[___QualityName]);
                    if (q != null)
                    {
                        __result = q;
                        Debug.Log($"[QEffectOverTime] 成功 GetbyId！");
                        return false;
                    }

                    // 思路是这样的，原来是靠GetByName，且是获取不到的情况下，这种情况下文件就不能将Hull翻译，所以肯定为null，这里加个不为空的条件，即免去没有翻译的情再多此一举
                    // var HullGetbyID = QualityRepository.Instance.GetById(102029);
                    // if (HullGetbyID != null && __result == null)
                    // {
                    //    __result = HullGetbyID;
                    //    __instance.gameObject.SetActive(true);
                    // }
                }
                return true;
            }
            return true;
        }
    }
}
