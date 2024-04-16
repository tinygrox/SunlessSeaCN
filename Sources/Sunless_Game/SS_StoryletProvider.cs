using HarmonyLib;
using Sunless.Game.ApplicationProviders;
using System;

namespace SSTranslator.Sunless_Game
{
    [HarmonyPatch(typeof(StoryletProvider))]
    public class SS_StoryletProvider
    {
        [HarmonyPostfix]
        [HarmonyPatch(MethodType.Constructor)]
        static void SSPatch_StaticField(FailBetter.Core.Event ____defaultStorylet)
        {
            try
            {
                if (____defaultStorylet.Name.Equals("No storylet to display"))
                {
                    ____defaultStorylet.Name = "无可展示的故事";
                }
                if (____defaultStorylet.Description.Equals("There is currently no storylet to display."))
                {
                    ____defaultStorylet.Description = "这里当前无可展示的故事";
                }
            }
            catch (Exception e)
            {
                throw new Exception("[SSTranslator]有地方没能汉化到，出错信息为：", e);
            }
        }
    }
}
