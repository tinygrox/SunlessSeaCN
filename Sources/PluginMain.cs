using BepInEx;
using HarmonyLib;
using Sunless.Game.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using JsonFx.Json;

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
            HarmonyInstance = new Harmony("tinygrox.SunlessSeaChineseTranslator");
            JsonReader reader = new JsonReader();
            string json = File.ReadAllText(Path.Combine(SS_Utility.dataPath, "qualities.json"));
            SS_Utility.Name2Id = reader.Read<Dictionary<string, int>>(json);
            SS_Utility.LoadAllTextures();
            //try
            //{
            HarmonyInstance.PatchAll();
            //}
            //catch (Exception ex)
            //{
            //Logger.LogError($"[Some Harmony Patches failed]:\n {ex.Message}");
            //}

            Logger.LogInfo("Harmony Pathes Applied For Sunless Sea!");
        }
    }
}
