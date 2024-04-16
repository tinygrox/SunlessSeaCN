using FailBetter.Core;
using HarmonyLib;
using Sunless.Game.Data.SNRepositories;
using Sunless.Game.Phenomena.QualityEffects;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(QEffectOnEnter))]
    public class SS_QEffectOnEnter
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(QEffectOnEnter.TheQuality), MethodType.Getter)]
        public static bool SSPatch_fuckGetByName(ref Quality __result, string ___QualityName)
        {
            if (QualityRepository.Instance.GetByName(___QualityName) == null)
            {
                Debug.Log($"[QEffectOnEnter] -TheQuality 首先 {___QualityName} 是 null 的");
                if (SS_Utility.Name2Id.ContainsKey(___QualityName))
                {

                    Debug.Log($"[QEffectOnEnter] 字典里有 {___QualityName}！");
                    var q = QualityRepository.Instance.GetById(SS_Utility.Name2Id[___QualityName]);
                    if (q != null)
                    {

                        Debug.Log($"[QEffectOnEnter] 成功 GetbyId！");
                        __result = q;
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}
