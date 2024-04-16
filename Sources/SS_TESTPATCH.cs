using HarmonyLib;

namespace SSTranslator
{
    [HarmonyPatch]
    public class SS_TESTPATCH
    {
        //// Splash Screen
        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(IntroScript), "Update")]
        //static bool Postfix()
        //{
        //    return false;
        //}
        //[HarmonyPrefix]
        //[HarmonyPatch(typeof(IntroScript), "LoadTitleScreen")]
        //static bool PostfixLoadTitleScreen()
        //{
        //    return false;
        //}
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
