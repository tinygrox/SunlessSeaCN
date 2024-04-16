using HarmonyLib;
using Sunless.Game.Dictionaries;
using Sunless.Game.Entities;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(InputController))]
    public class SS_InputController
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(InputController.CurrentScheme), MethodType.Getter)]
        public static void SSPatch_CurrentScheme()
        {
            for (int i = 0; i < DefaultControlSchemes.Default.Count; i++)
            {
                var keyName = DefaultControlSchemes.Default[i].Name;
                if (SS_Utility.KeyScheme.ContainsKey(keyName))
                {
                    DefaultControlSchemes.Default[i].Name = SS_Utility.KeyScheme[keyName];
                }
            }
        }
        // 每次进入函数前，都修改一下传入的参数，保证请求到的是对应的翻译的按键。下同
        [HarmonyPrefix]
        [HarmonyPatch(nameof(InputController.GetKeyDown), new[] { typeof(string) })]
        public static void SSPatch_GetKeyDown(ref string keyname)
        {
            if (SS_Utility.KeyScheme.ContainsKey(keyname))
                keyname = SS_Utility.KeyScheme[keyname];
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(InputController.GetKeyUp), new[] { typeof(string) })]
        public static void SSPatch_GetKeyUp(ref string keyname)
        {
            if (SS_Utility.KeyScheme.ContainsKey(keyname))
                keyname = SS_Utility.KeyScheme[keyname];
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(InputController.GetKey), new[] { typeof(string) })]
        public static void SSPatch_GetKey(ref string keyname)
        {
            if (SS_Utility.KeyScheme.ContainsKey(keyname))
                keyname = SS_Utility.KeyScheme[keyname];
        }
        [HarmonyPrefix]
        [HarmonyPatch(nameof(InputController.GetTooltipValue), new[] { typeof(string) })]
        public static void SSPatch_GetTooltipValue(ref string keyname)
        {
            if (SS_Utility.KeyScheme.ContainsKey(keyname))
                keyname = SS_Utility.KeyScheme[keyname];
        }
    }
}
