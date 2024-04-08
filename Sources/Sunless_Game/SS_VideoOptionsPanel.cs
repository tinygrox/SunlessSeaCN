using HarmonyLib;
using Sunless.Game.PlayerPrefDictionaries;
using Sunless.Game.UI.Menus.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(VideoOptionsPanel))]
    public class SS_VideoOptionsPanel
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject), typeof(bool)])]
        public static void SSPatch_VideoOptionsPanel(GameObject parent)
        {
            //Debug.Log($"{parent.name}"); // 期望是 Achor_CENTER

            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/Title", "Video settings", "视频设置");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/Options/Quality/Label", "Quality:", "质量：");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/Options/Resolution/Label", "Resolution:", "分辨率：");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/Options/WindowMode/Label", "Window Mode:", "窗口模式：");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/Options/Effects/Label", "Screen Effects:", "屏幕特效：");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/Options/UISize/Label", "UI scale:", "UI 缩放：");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/Options/FontSize/Label", "Font scale:", "字体缩放：");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/Options/FontExample/FontSizeExample", "Font size example", "字体大小示例");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/FooterButtons/AcceptButton/Text", "Accept", "应用");
            SS_Utility.UpdateGameObjectText(parent, "VideoOptionsPanel(Clone)/FooterButtons/CancelButton/Text", "Cancel", "取消");
        }

        [HarmonyPrefix]
        [HarmonyPatch("SetQualityOptions")]
        static void SSPatch_SetQualityOptions()
        {
            PlayerPrefOptions.SupportedQualityOptions.Clear();
            PlayerPrefOptions.SupportedQualityOptions = new Dictionary<string, int>()
            {
                ["低"] = 1,
                ["中"] = 2,
                ["高"] = 3
            };
        }
        [HarmonyPrefix]
        [HarmonyPatch("SetWindowOptions")]
        static void SSPatch_SetWindowOptions(ref string[] ____windowOptions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Fullscreen"] = "全屏",
                ["Windowed"] = "窗口化",
            };
            for (int i = 0; i < ____windowOptions.Length; i++)
            {
                if (trans.ContainsKey(____windowOptions[i]))
                    ____windowOptions[i] = trans[____windowOptions[i]];
            }
        }
        [HarmonyPrefix]
        [HarmonyPatch("SetEffectsOptions")]
        static void SSPatch_SetEffectsOptions(ref string[] ____effects)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Enabled"] = "启用",
                ["Disabled"] = "禁用",
            };
            for (int i = 0; i < ____effects.Length; i++)
            {
                if (trans.ContainsKey(____effects[i]))
                    ____effects[i] = trans[____effects[i]];
            }
        }

    }
}
