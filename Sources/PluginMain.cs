using BepInEx;
using HarmonyLib;
using Sunless.Game.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using JsonFx.Json;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SSTranslator
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Sunless Sea.exe")]
    public class PluginMain : BaseUnityPlugin
    {
        Harmony HarmonyInstance;
        private void Awake()
        {
            // Plugin startup
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            SS_Utility.LoadAllTextures();
            JsonReader reader = new JsonReader();
            string json = File.ReadAllText(Path.Combine(SS_Utility.dataPath, "qualities.json"));
            SS_Utility.Name2Id = reader.Read<Dictionary<string, int>>(json);

            // 无光之海的开头启动画面相关的 class 名为 IntroScript，默认 BepInEx 的 EntryPoint 介入不到（结果就是 BepInEx 会在该类之后才载入）
            // 所以需要修改BepInEx/config/BepInEx.cfg配置文件，Assembly = Sunless.Game.dll、Type = IntroScript、Method = PlayEAWarning
            // 这样 BepInEx 就会在这个方法调用时就载入，完成启动画面的修改。
            var splashimage = GameObject.Find("EAWarning")?.transform;
            if (splashimage != null)
            {
                Debug.Log("EAWarning!");
                var oldSprite = splashimage.GetComponent<Image>();
                if (oldSprite != null)
                {
                    Debug.Log("EAWarning[Sprite]");
                    oldSprite.sprite = SS_Utility.GetSprite(splashimage, "splash-screen.png");
                }
            }

            HarmonyInstance = new Harmony("tinygrox.SunlessSeaChineseTranslator");
            HarmonyInstance.PatchAll();

            Logger.LogInfo("Harmony Pathes Applied For Sunless Sea!");
        }
    }
}
