using HarmonyLib;
using Sunless.Game.ExtensionMethods;
using System.Collections.Generic;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(KeycodeExtensionMethods))]
    public class SS_KeycodeExtensionMethods
    {
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(KeycodeExtensionMethods.HumanReadable), new[] { typeof(string) })]
        public static IEnumerable<CodeInstruction> SSPatch_HumanReadable(IEnumerable<CodeInstruction> instructions)
        {
            var trans = new Dictionary<string, string>()
            {
                ["Up"] = "↑",
                ["Down"] = "↓",
                ["Left"] = "←",
                ["Right"] = "→"
            };
            SS_Utility.ILReplacer(instructions, trans);
            return instructions;
        }
        // 键位映射翻译
        static Dictionary<string, string> KeyCode2CN = new Dictionary<string, string>()
        {
            ["Space"] = "空格键",
            ["LeftShift"] = "左 Shift",
            ["RightShift"] = "右 Shift",
            ["LeftControl"] = "左 Ctrl",
            ["RightControl"] = "右 Ctrl",
            ["LeftAlt"] = "左 Alt",
            ["RightAlt"] = "右 Alt",
            ["Mouse0"] = "鼠标左键",
            ["Mouse1"] = "鼠标右键",
            ["Mouse2"] = "鼠标中键",
            ["Mouse3"] = "鼠标键位 3",
            ["Mouse4"] = "鼠标键位 4",
            ["Mouse5"] = "鼠标键位 5",
            ["Mouse6"] = "鼠标键位 6",
            ["Backspace"] = "退格键",
            ["Return"] = "回车",
            ["Keypad0"] = "小键盘 0",
            ["Keypad1"] = "小键盘 1",
            ["Keypad2"] = "小键盘 2",
            ["Keypad3"] = "小键盘 3",
            ["Keypad4"] = "小键盘 4",
            ["Keypad5"] = "小键盘 5",
            ["Keypad6"] = "小键盘 6",
            ["Keypad7"] = "小键盘 7",
            ["Keypad8"] = "小键盘 8",
            ["Keypad9"] = "小键盘 9",
            ["KeypadPeriod"] = "小键盘 .",
            ["KeypadDivide"] = "小键盘 /",
            ["KeypadMultiply"] = "小键盘 *",
            ["KeypadMinus"] = "小键盘 -",
            ["KeypadPlus"] = "小键盘 +",
            ["KeypadEnter"] = "小键盘回车",
            ["KeypadEquals"] = "小键盘 =",
            ["Equals"] = "=",
            ["Comma"] = ",",
            ["Minus"] = "-",
            ["Period"] = ".",
            ["Slash"] = "/",
            ["Semicolon"] = ";",
            ["Quote"] = "'",
            ["Backslash"] = "\\",
            ["LeftBracket"] = "[",
            ["RightBracket"] = "]",
            ["BackQuote"] = "`",
        };
        [HarmonyPostfix]
        [HarmonyPatch(nameof(KeycodeExtensionMethods.HumanReadable), new[] { typeof(string) })]
        public static void SSPostPatch_HumanReadable(ref string __result)
        {
            if (KeyCode2CN.ContainsKey(__result))
            {
                __result = KeyCode2CN[__result];
            }
        }

    }

}
