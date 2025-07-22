using UnityEngine;
using UnityEngine.UI;

namespace Game.Configs.ClickButtonConfigs
{
    [CreateAssetMenu(menuName = "Configs/ClickButtonConfig", fileName = "ClickButtonConfig")]
    public class ClickButtonConfig : ScriptableObject
    {
        public Sprite DefaultSprite;
        public ColorBlock ButtonColors;
    }
}