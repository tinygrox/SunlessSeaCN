using HarmonyLib;
using Sunless.Game.Entities;
using Sunless.Game.UI.Tutorial;
using Sunless.Game.Unity_Extensions.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SSTranslator
{
    [HarmonyPatch]
    public class SS_TESTPATCH
    {
        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(TutorialContainerPanel), nameof(TutorialContainerPanel.AddTutorial), [typeof(Tutorial)])]
        //public static void SSPatch_TutorialContainerPanelPrefix(Tutorial tutorial)
        //{
        //    Debug.LogWarning($"[TutorialContainerPanel]:AddTutorial byID: [Id = {tutorial.Id}, Name = {tutorial.Name}]");
        //}


        // 查明是 Unity Expolorer 所导致的无法取消右键菜单，按 F7 即可
        //[HarmonyPostfix]
        //[HarmonyPatch(typeof(PopupMenu), "Update")]
        //public static void SSPatch_PopupMenuUpdate(bool ____isVisible, PopupMenu ____instance)
        //{
        //    if (Input.GetKeyUp(KeyCode.Mouse0))
        //    {
        //        Debug.Log($"[_isVisible] is {____isVisible}!");
        //        if (____instance != null)
        //        {
        //            Debug.Log($"{____instance} not null");
        //        }
        //        else
        //            Debug.Log($"{____instance} is null!");
        //    }
        //}

    }
}
