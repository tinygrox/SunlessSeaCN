using HarmonyLib;
using Sunless.Game.Entities;
using Sunless.Game.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    /// <summary>
    /// 两种方法实现，都可以
    /// </summary>
    [HarmonyPatch(typeof(LoadingScreen))]
    public class SS_LoadingScreen
    {
        // 直接重新随机返回
        //[HarmonyPostfix]
        //[HarmonyPatch("SetupBackground")]
        //static void LoadingScreenTips()
        //{
        //    GameObject.Find("Tip").GetComponent<Text>().text = SS_Utility.CNTips[UnityEngine.Random.Range(0, SS_Utility.CNTips.Count<string>())];
        //}

        [HarmonyTranspiler]
        [HarmonyPatch("SetupBackground")]
        public static IEnumerable<CodeInstruction> SSPatch_SetupBackground(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();
            int startIndex = -1;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldstr && codes[i].operand.ToString().Equals("Tip"))
                {
                    startIndex = i; // 找到待插入位置
                    break;
                }
            }

            if (startIndex > -1)
            {
                CodeInstruction call = CodeInstruction.Call(typeof(SS_LoadingScreen), nameof(SS_LoadingScreen.SetCNLoadingTips));
                codes.Insert(startIndex, call); // 在此处插入，当前后续指令向后移
            }

            return codes.AsEnumerable();
        }

        // 待插入的函数
        public static void SetCNLoadingTips()
        {
            //Debug.LogWarning($"[SS_Translator] SetCNLoadingTips Equals? - {StaticEntities.LoadingScreenTips == SS_Utility.CNTips}");
            if (StaticEntities.LoadingScreenTips != SS_Utility.CNTips)
                StaticEntities.LoadingScreenTips = SS_Utility.CNTips;
            //Debug.LogWarning($"[SS_Translator] SetCNLoadingTips And NOW? - {StaticEntities.LoadingScreenTips == SS_Utility.CNTips}");
        }
    }
}
