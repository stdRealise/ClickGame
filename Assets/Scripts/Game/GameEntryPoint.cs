using Game.Configs.LevelConfigs;
using Game.Configs.SkillsConfig;
using Game.Configs.AttackSyConfig;
using Game.Enemies;
using Game.ClickButton;
using Game.Skills;
using Global.Audio;
using Global.SaveSystem;
using Global.SaveSystem.SavableObjects;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using static Global.Audio.AudioManager;


namespace Game
{
    public class GameEntryPoint : EntryPoint
    {
        [SerializeField] private ClickButtonManager _clickButtonManager;
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private Timer.Timer _timer;
        [SerializeField] private EndLevelWindow.EndLevelWindow _endLevelWindow;
        [SerializeField] private Image _background;
        [SerializeField] private LevelsConfig _levelsConfig;
        [SerializeField] private SkillsConfig _skillsConfig;
        [SerializeField] private AttackSyConfig _attackSyConfig;

        private GameEnterParams _gameEnterParams;
        private SaveSystem _saveSystem;
        private AudioManager _audioManager;
        private SkillSystem _skillSystem;
        private EndLevelSystem _endLevelSystem;
        private SceneLoader _sceneLoader;



        private const string COMMON_OBJECT_TAG = "CommonObject";
        public override void Run(SceneEnterParams enterParams)
        {
            var commonObject = GameObject.FindWithTag(COMMON_OBJECT_TAG).GetComponent<CommonObject>();
            _saveSystem = commonObject.SaveSystem;
            _audioManager = commonObject.AudioManager;
            _sceneLoader = commonObject.SceneLoader;
            if (enterParams is not GameEnterParams gameEnterParams)
            {
                Debug.LogError("enter params trouble");
                return;
            }
            _gameEnterParams = gameEnterParams;

            
            _enemyManager.Initialize(_healthBar, _timer);
            _endLevelWindow.Initialize();

            var openedSkills = (OpenedSkills)_saveSystem.GetData(SavableObjectType.OpenedSkills);
            _skillSystem = new(openedSkills, _skillsConfig, _enemyManager, _attackSyConfig);
            _clickButtonManager.Initialize(_skillSystem);
            _endLevelSystem = new(_endLevelWindow, _saveSystem, _gameEnterParams, _levelsConfig);
            _endLevelWindow.OnRestartClicked += RestartLevel;
            _endLevelWindow.OnMetaClicked += GoToMeta;
            _endLevelWindow.OnNextLvlClicked += GoToNextLvl;
            _audioManager.PlayClip(AudioNames.FightMusic);
            _enemyManager.OnLevelCompleted += _endLevelSystem.LevelCompleted;
            StartLevel();
        }

        private void StartLevel()
        {
            var maxLocationAndLevel = _levelsConfig.GetMaxLocationAndLevel();
            var location = _gameEnterParams.Location;
            var level = _gameEnterParams.Level;
            if (location > maxLocationAndLevel.x ||
               (location == maxLocationAndLevel.x && level > maxLocationAndLevel.y))
            {
                location = maxLocationAndLevel.x;
                level = maxLocationAndLevel.y;
            }
            _endLevelWindow.SetEndLvlParams(_levelsConfig.GetLevel(location, level));
            _background.sprite = _levelsConfig.GetLevelBg(location, level);
            var levelData = _levelsConfig.GetLevel(_gameEnterParams.Location, _gameEnterParams.Level);
            _enemyManager.StartLevel(levelData);
        }

        private void RestartLevel()
        {
            _sceneLoader.LoadGameplayScene(_gameEnterParams);
        }

        private void GoToMeta()
        {
            _sceneLoader.LoadMetaScene();
        }

        private void GoToNextLvl()
        {
            var nextLvl = (Progress)_saveSystem.GetData(SavableObjectType.Progress);
            GameEnterParams nextParams = new GameEnterParams(nextLvl.CurrentLocation, nextLvl.CurrentLevel);
            _sceneLoader.LoadGameplayScene(nextParams);
        }
    }
}