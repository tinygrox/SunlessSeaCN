using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace SSTranslator
{
    [HarmonyPatch(typeof(Sunless.Game.Scripts.Menus.TitleScreenInit))]
    public class SS_TitleScreenInit
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void SSPatch_Anchors()
        {
            //Debug.Log($"Fingding 'TitlePanel'.....");
            Transform child = GameObject.Find("TitlePanel").transform.GetChild(0);
            if (child != null)
            {
                Image img1 = child.GetComponent<Image>();
                if (img1 != null)
                {
                    var texture1 = SS_Utility.GetImageTexture2D("Title.png");
                    if (texture1 != null) // 防止某些人手贱删除，然后就没了
                    {
                        var oldsprite = img1.sprite;
                        Sprite mysprite = Sprite.Create(texture1, oldsprite.rect, new Vector2(oldsprite.pivot.x / oldsprite.rect.width, oldsprite.pivot.y / oldsprite.rect.height), oldsprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, oldsprite.border);
                        img1.sprite = mysprite;
                    }
                }
            }
            Transform child2 = GameObject.Find("TitlePanel").transform.GetChild(1);
            if (child2 != null)
            {
                //Debug.Log($"child2 is find and not null!");
                Image img2 = child2.GetComponent<Image>();
                if (img2 != null)
                {
                    //Debug.Log($"child2's image is not null!");
                    Texture2D texture2 = SS_Utility.GetImageTexture2D("TitleZMNew.png");
                    var oldsprite = img2.sprite;
                    if (texture2 != null)
                    {
                        texture2.filterMode = FilterMode.Trilinear;
                        texture2.wrapMode = TextureWrapMode.Clamp;
                        //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                        Sprite mysprite = Sprite.Create(texture2, oldsprite.rect, new Vector2(oldsprite.pivot.x / oldsprite.rect.width, oldsprite.pivot.y / oldsprite.rect.height), oldsprite.pixelsPerUnit, 0, SpriteMeshType.FullRect, oldsprite.border);
                        //Debug.Log($"my sprite is not null!");
                        img2.sprite = mysprite;
                    }
                }
            }
        }
    }
}
