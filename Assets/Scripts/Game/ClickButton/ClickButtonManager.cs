using Game.Skills;
using UnityEngine;
using UnityEngine.Events;
using Game.Configs.ClickButtonConfigs;

namespace Game.ClickButton
{
    public class ClickButtonManager : MonoBehaviour
    {
        [SerializeField] private ClickButton _clickStableButton;
        [SerializeField] private ClickButton _clickHaosButton;
        [SerializeField] private ClickButton _clickSuppresionButton;
        [SerializeField] private ClickButton _clickExpansionButton;
        [SerializeField] private ClickButtonConfig _buttonStableConfig;
        [SerializeField] private ClickButtonConfig _buttonHaosConfig;
        [SerializeField] private ClickButtonConfig _buttonSuppresionConfig;
        [SerializeField] private ClickButtonConfig _buttonExpansionConfig;

        public void Initialize(SkillSystem skillSystem)
        {
            _clickStableButton.Initialize(_buttonStableConfig.DefaultSprite, _buttonStableConfig.ButtonColors);
            _clickHaosButton.Initialize(_buttonHaosConfig.DefaultSprite, _buttonHaosConfig.ButtonColors);
            _clickSuppresionButton.Initialize(_buttonSuppresionConfig.DefaultSprite, _buttonSuppresionConfig.ButtonColors);
            _clickExpansionButton.Initialize(_buttonExpansionConfig.DefaultSprite, _buttonExpansionConfig.ButtonColors);
            _clickStableButton.SubscribeOnClick(() => skillSystem.InvokeTrigger(SkillTrigger.OnStable));
            _clickHaosButton.SubscribeOnClick(() => skillSystem.InvokeTrigger(SkillTrigger.OnHaos));
            _clickSuppresionButton.SubscribeOnClick(() => skillSystem.InvokeTrigger(SkillTrigger.OnSuppression));
            _clickExpansionButton.SubscribeOnClick(() => skillSystem.InvokeTrigger(SkillTrigger.OnExpansion));
        }
    }
}