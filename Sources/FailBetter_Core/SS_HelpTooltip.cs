using FailBetter.Core.Help;
using HarmonyLib;

namespace SSTranslator.FailBetter_Core
{
    [HarmonyPatch(typeof(HelpTooltip))]
    public class SS_HelpTooltip
    {
        [HarmonyPrefix]
        [HarmonyPatch(MethodType.Constructor, [typeof(string)])]
        public static void SSPatch_HelpTooltip(ref string message)
        {
            if (message == "The higher the Quality, the higher the chance of success.")
                message = "特质数值越高，成功的机会就越大。";
        }
    }
}
