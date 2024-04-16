using HarmonyLib;
using System;

namespace SSTranslator
{
    [HarmonyPatch]
    public class SS_BranchPanel
    {
        [HarmonyPostfix]
        [HarmonyPatch(typeof(Sunless.Game.UI.Storylet.BranchPanel), MethodType.Constructor, [typeof(UnityEngine.GameObject), typeof(FailBetter.Core.Branch), typeof(Sunless.Game.Entities.SunlessCharacter)])]
        static void Postfix(ref UnityEngine.UI.Text ____goButtonText)
        {
            try
            {
                if (____goButtonText.text.Equals("Go"))
                {
                    ____goButtonText.text = "继续";
                }
                else if (____goButtonText.text.Equals("Locked"))
                {
                    ____goButtonText.text = "已锁定";
                }
            }
            catch (Exception e)
            {
                throw new Exception("[SSTranslator]有地方没能汉化到，出错信息为：", e);
            }
        }
    }
}
