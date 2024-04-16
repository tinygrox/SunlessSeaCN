using HarmonyLib;
using Sunless.Game.UI.Tutorial;
using UnityEngine;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(TutorialContainerPanel))]
    public class SS_TutorialContainerPanel
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor, [typeof(GameObject)])]
        public static void SSPatch_TutorialContainerPanel(GameObject parent)
        {
            //Debug.LogWarning($"[TutorialContainerPanel] parent is {parent.name}, expect 'BL_Table'");
            SS_Utility.PatchHelper(() =>
            {
                SS_Utility.UpdateGameObjectText(parent, "TutorialPanel(Clone)/Title", "TUTORIAL", "教程");
                SS_Utility.UpdateGameObjectText(parent, "TutorialPanel(Clone)/Disable/Text", "Disable tutorial", "禁用教程");
                SS_Utility.UpdateGameObjectText(parent, "TutorialPanel(Clone)/Close/Text", "Close", "关闭");
            });
        }
    }
}
