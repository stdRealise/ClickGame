using Game.Configs.EnemyConfigs;
using Game.Configs.SkillsConfig;
using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using Meta.Locations;
using Meta.Shop;
using SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

namespace Meta
{
    public class MetaEntryPoint : EntryPoint
    {
        [SerializeField] private LocationManager _locationManager;
        [SerializeField] private ShopWindow _shopWindow;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private Button _switchButton;
        //[SerializeField] private Button _rewardedButton;
        [SerializeField] private TextMeshProUGUI _money;

        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SceneLoader _sceneLoader;
        private const string COMMON_OBJECT_TAG = "CommonObject";
        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            _audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;

            var progres = (Progress) _saveSystem.GetData(SavableObjectType.Progress);
            _locationManager.Initialize(progres, StartLevel);
            _shopWindow.Initialize(_saveSystem, _skillsConfig);
            _money.text = ((Wallet) _saveSystem.GetData(SavableObjectType.Wallet)).Coins.ToString();
            _switchButton.onClick.AddListener(SwitchCanvases);
            _audioManager.PlayClip(AudioNames.MetaMusic);
        }
        private void StartLevel(int location, int level) 
        {
            _sceneLoader.LoadGameplayScene(new GameEnterParams(location, level));
        }

        private void SwitchCanvases()
        {
            YG2.InterstitialAdvShow();
            bool showShop = !_shopWindow.gameObject.activeSelf;
            _locationManager.gameObject.SetActive(!showShop);
            _shopWindow.gameObject.SetActive(showShop);
        }
    }
}

