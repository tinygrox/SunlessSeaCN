using HarmonyLib;
using Sunless.Game.Import;
using Sunless.Game.UI.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(Importer))]
    public class SS_Importer
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(Importer.BeginImport), [typeof(IProgressBar), typeof(Action), typeof(Action)])]
        public static IEnumerable<CodeInstruction> SSPatch_BeginImport(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Content is up to date", "内容为最新");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("Download")]
        public static IEnumerable<CodeInstruction> SSPatch_Download(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Downloading...", "正在下载…");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("DownloadImages")]
        public static IEnumerable<CodeInstruction> SSPatch_DownloadImages(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Downloading Images...", "正在下载图片…");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("VerifyDownload")]
        public static IEnumerable<CodeInstruction> SSPatch_VerifyDownload(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Verifying download...", "验证下载内容…");
            });
            return instructions;
        }
        [HarmonyTranspiler]
        [HarmonyPatch("Import")]
        public static IEnumerable<CodeInstruction> SSPatch_Import(IEnumerable<CodeInstruction> instructions)
        {
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.ILReplacer(ref instructions, "Importing...", "正在导入…");
            });
            return instructions;
        }
    }
}
