using HarmonyLib;
using Sunless.Game.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator
{
    public static class SS_Utility
    {
        // 前人的遗留之物……
        public static Dictionary<string, int> Name2Id;
        public static string[] CNTips = ["在海中修理船只需要消耗补给, 在伦敦修理船只花销更低", "海泽从不宽容, 对你也一样", "如果你的敌人被照亮了, 那么你的开火进程(武器按键周围荧光)会加快", "海怪会在攻击间隔下潜(或散开),在它们上浮的时候抓住机会进行攻击", "感叹号意味着你的敌人即将发起攻击, 尽可能躲到他们的攻击范围之外", "提升面纱值可以让敌人不那么容易发现你. 关闭前灯也可以让你隐匿行踪(同时节省燃料).", "活冰山不会下潜, 但是如果它们开始蓄力准备撞击你, 你知道该怎么做的", "当你的射击按钮周围是橙色时，你的敌人不会被照亮，当被照亮时将会变成绿色，此时增长速度会加快", "点击敌人来锁定目标, 确保你的目标在武器的攻击范围内, 这样开火进程才会增长", "所有的船只都装备有大射界的甲板炮, 部分船只装备有威力更大射程更远, 但是射界较小的主炮", "你无法越过海岛开火, 毕竟那上面可能有人居住", "打开前灯可以减慢恐惧增加的速度, 但也会把你暴露给敌人, 并消耗更多燃料", "大多数敌人无法对后方目标进行攻击", "没有燃料的话, 你的前灯和引擎都无法工作", "你可以在开火进程完成前开火, 但是这会影响命中率", "所有的事情都从陷落的伦敦开始, 你可以在那里找到新的船员和故事", "感到赚钱很难? 试试为海军工作.", "感到赚钱很难? 试试文登湾或者珊瑚之源的工作", "只要你有补给, 船员的饥饿度会自动消除. 但是如果补给耗尽, 你和船员都将被饥饿折磨", "长时间的航海会让你的船员, 还有你, 因恐惧而失去理智. 保持前灯开启可以减缓恐惧蔓延, 但也会消耗更多燃料", "如果船员数量降低到0, 你也会死", "如果船体结构降低到0, 你的船会沉没", "升级你的住处并订立遗嘱, 可以把你的财产传递.给继承人. 不过这不意味着他们会感谢你", "鱼枪类武器对付海怪更有效, 火炮则更适合对付船只", "新汗国处于内战中: 可汗之影是可汗之心的敌人", "你可以在开火进程完成前开火, 但是这会降低命中率", "小商人很难能从公开市场上交易赚到钱，寻找故事事件来更便宜地获取货物", "在钢铁共和国, 那红光喜欢心智的烟雾, 吞噬雨水", "帕布拉亚麻来自于镜子后的某处", "你可以把死亡侨民卖往死亡租界, 放心, 这不是贩卖人口, 他们甚至还会给你小费 ", "梦中藏有真理, 也潜伏着危险.", "啊, 伦敦的灯火! 如果你的恐惧高于50, 那么在有事件等待你的时候回到伦敦会消除高于50部分的恐惧", "珊瑚之源喜欢下棋", "在地下生活了太长时间的你无法在地表生活, 然而你还是可以短暂回到地表", "你的引擎功率越大, 你的船就越快, 消耗燃料也越多. 你的船越重, 那么你的航行速度就越慢", "大多数船只装备卸下后会占用1点货仓空间", "引擎在过载的时候可以让你的船更快!-但是也可能会爆炸,造成戏剧性的结果", "海泽北方被冰墙封锁", "伦敦陨落会补充无光之海的背景故事,有时还会为你解锁新故事. 那里还有许多可爱小猫.", "汗国充满了认为伦敦人是外国人的外国人. 不要相信他们", "叛国者女王正式场合会被称为 '不朽的女王陛下'.", "奇怪的猎获物可以用于把海怪吸引上水面.", "前灯可以标记海中的标志性地点, 减缓恐惧增加, 照亮敌人, 但也会增加燃料消耗", "气动老鼠输送机可以让你在战斗中进行修理.", "一旦你得到了潜艇, 之后所有的新船长都可以直接使用潜艇", "你的声呐会指明附近的任何港口所在方向", "你的声呐可以点亮海底值得注意的东西", "海蝙蝠可以探测到水下的定居点. 请下潜寻找这些地点", "高恐惧的时候下潜需要格外小心.有生物可以嗅到船员们的恐惧."];

        public static string[] CNShipNames = ["夏洛特", "迈那得斯", "纳古拉格斯特", "玛蒂尔达 布里格斯", "伊芙吉娜", "赫卡柏", "无恙", "勇气", "流浪", "巴斯克维尔", "忧郁", "旖旎", "斑点", "撒马尔罕", "朝圣", "忒休斯", "扎希尔", "拜占庭", "鳄鱼", "凤凰", "注定", "蔚蓝", "朱砂", "比阿特丽斯", "黄玉", "巴托里", "基尔戈", "穆加特罗伊德", "乌木", "帕凡", "阿斯忒里翁", "骆驼", "节制", "红艳", "公正", "狂暴", "铭记", "自信", "魔法师", "塞浦路斯", "阿尔法", "克里德莫尔", "赛斯", "奇尼丝", "孤独", "葛洛莉安", "阿德拉斯忒亚", "奇平诺顿", "克吕泰墨斯特拉", "赫斯珀里得斯", "严格", "夜曲", "背誓者", "希尔芙", "火蜥蜴", "温蒂尼", "罗蕾莱", "聂瑞易德斯", "巨蛇", "旅居者", "希望", "潘多拉", "普罗米修斯", "克诺索斯", "星火"];

        public static Dictionary<string, string> KeyScheme = new()
        {
            ["Forward"] = "向前",
            ["Backward"] = "向后",
            ["Left"] = "向左",
            ["Right"] = "向右",
            ["Chart"] = "海图",
            ["Gazetteer"] = "日志",
            ["Transform Ship"] = "切换船型",
            ["Zeebat"] = "海蝙蝠",
            ["Lights"] = "灯光照明",
            ["Repair"] = "修理",
            ["Turbo"] = "增压提速",
            ["Use"] = "使用",
            ["Horn"] = "鸣笛",
            ["PauseResume"] = "暂停/恢复",
            ["Target"] = "目标", // 忘记是什么了
            ["Scroll Up"] = "向上滚动",
            ["Scroll Down"] = "向下滚动",
            ["Stack Item"] = "堆叠整理物品",
            ["Deck Weapon"] = "甲板武器",
            ["Forward Weapon"] = "船艏武器",
            ["Aft Weapon"] = "舰艉武器",
            ["Combat Item 1"] = "战斗物品 1",
            ["Combat Item 2"] = "战斗物品 2",
            ["Combat Item 3"] = "战斗物品 3",
            ["Combat Item 4"] = "战斗物品 4",
            ["Combat Item 5"] = "战斗物品 5",
            ["Combat Item 6"] = "战斗物品 6"
        };
        public static IEnumerable<CodeInstruction> ILReplacer(ref IEnumerable<CodeInstruction> codes, string source, string target)
        {
            var codeList = codes.ToList();
            bool found = false;
#if DEBUG
            string pMethod = "Unknow";
#endif
            foreach (var code in codeList.Where(code => code.opcode == OpCodes.Ldstr && code.operand.ToString().Equals(source)))
            {
                found = true;
#if DEBUG
                StackTrace st = new();
                StackFrame[] stackframes = st.GetFrames();
                if (stackframes.Length > 2)
                {
                    StackFrame parentFrame = stackframes[1];
                    pMethod = parentFrame.GetMethod().Name;
                }
                UnityEngine.Debug.Log($"[SSTranslator] <{pMethod}> String 【{source}】->【{target}】");
#endif
                code.operand = target;
            }
            if (!found)
            {
#if DEBUG

                UnityEngine.Debug.LogError($"[SSTranslator] <{pMethod}> ILReplacer String '{source}' not found for replacement.");
#else
                UnityEngine.Debug.LogError($"[SSTranslator] ILReplacer String '{source}' not found for replacement.");
#endif
            }
            codes = codeList;
            return codes;
        }

        public static IEnumerable<CodeInstruction> ILReplacer(IEnumerable<CodeInstruction> codes, Dictionary<string, string> translationDict, bool breakOnceFound = false)
        {
            var codeList = codes.ToList();
            bool found = false;
            foreach (var code in codeList.Where(code => code.opcode == OpCodes.Ldstr && translationDict.ContainsKey(code.operand.ToString())))
            {
                found = true;

#if DEBUG
                string pMethod = "Unkonw";
                StackTrace st = new();
                StackFrame[] stackframes = st.GetFrames();
                if (stackframes.Length > 2)
                {
                    StackFrame parentFrame = stackframes[1];
                    pMethod = parentFrame.GetMethod().Name;
                }
                UnityEngine.Debug.Log($"[SSTranslator] <{pMethod}> String 【{code.operand}】->【{translationDict[code.operand.ToString()]}】");
#endif
                code.operand = translationDict[code.operand.ToString()];
                if (breakOnceFound)
                {
                    break;
                }
            }
            if (found)
                return codeList.AsEnumerable();
            else
            {
                UnityEngine.Debug.LogError($"[SSTranslator] ILReplacer String {translationDict.Keys.Select(key => key.ToString())} not found for replacement.");
                return codes;
            }
        }

        public static void PatchHelper(Action patchAction)
        {
            try
            {
                patchAction();
            }
            catch (Exception e)
            {
                //throw new Exception($"[SSTranslator] 有地方没能汉化到，且还出错了，出错信息为：{e}");
                UnityEngine.Debug.LogError($"[SSTranslator] 有地方没能汉化到，且还出错了，出错信息为：{e}");
                throw;
            }
        }
        public static void GetCNShipNames()
        {
            // 反正是public static的，直接改就完事
            if (StaticEntities.StartingShipNames != CNShipNames)
                StaticEntities.StartingShipNames = CNShipNames;
        }

        static Dictionary<string, Texture2D> TextureCache = new Dictionary<string, Texture2D>();
        private static string dll_Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string imagePath = Path.Combine(dll_Location, "Images");
        public static string dataPath = Path.Combine(dll_Location, "Data");

        /// <summary>
        /// 从插件路径下的Images文件夹下读取图片并转换为Texture2D，如果文件已缓存，则直接返回
        /// </summary>
        /// <param name="filename">带扩展名的png图片</param>
        /// <returns>null 或者 Texture2D</returns>
        public static Texture2D GetImageTexture2D(string filename)
        {
            if (TextureCache.ContainsKey(filename))
            {
                return TextureCache[filename];
            }
            Texture2D texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            texture.LoadImage(File.ReadAllBytes(Path.Combine(imagePath, filename)));
            if (texture != null)
            {
                //TextureCache.Add(filename, texture);
                return texture;
            }
            else
                return null;
        }

        public static void UpdateGameObjectText(GameObject parentObject, string path, string targetText, string replacementText)
        {
            var tmpObject = parentObject.transform?.Find(path);
            if (tmpObject != null)
            {
                var ObjText = tmpObject.GetComponent<Text>();
                if (ObjText != null && ObjText.text == targetText)
                {
                    ObjText.text = replacementText;
                }
                else
                {
                    UnityEngine.Debug.LogWarning($"[SSTranslator/UpdateGameObjectText] can not Find {path}.GetComponent<Text>() maybe {(ObjText == null ? "ObjText is null" : "ObjText.text != targetText")}!");
                }
            }
            else
            {
                UnityEngine.Debug.LogWarning($"[SSTranslator/UpdateGameObjectText] can not Find {path}!");
            }
        }

        public static Sprite GetSprite(Transform transform, string filename)
        {
            Sprite oldSprite = transform?.GetComponent<Image>()?.sprite;
            if (oldSprite != null)
            {
                Texture2D newTexture = GetImageTexture2D(filename);
                if (newTexture != null)
                {
                    Sprite mySprite = Sprite.Create(newTexture, oldSprite.rect, new Vector2(oldSprite.pivot.x / oldSprite.rect.width, oldSprite.pivot.y / oldSprite.rect.height), oldSprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, oldSprite.border);
                    return mySprite;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        /// 启动时扫描一遍 Images 下的 .png 图片，缓存成 Texture2D
        /// </summary>
        public static void LoadAllTextures()
        {
            TextureCache.Clear();
            foreach (string file in Directory.GetFiles(imagePath, "*.png", SearchOption.AllDirectories))
            {
                //UnityEngine.Debug.Log($"{Path.GetFileName(file)}");
                var filename = Path.GetFileName(file);
                TextureCache.Add(filename, GetImageTexture2D(filename));
            }
        }

    }
}
