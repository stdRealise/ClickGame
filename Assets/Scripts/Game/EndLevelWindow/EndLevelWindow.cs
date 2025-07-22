using Game.Configs.LevelConfigs;
using SceneManagement;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.EndLevelWindow
{
    public class EndLevelWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _loseLevelWindow;
        [SerializeField] private GameObject _winLevelWindow;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _goMetatButton;
        [SerializeField] private Button _nextLvlButton;
        [SerializeField] private TextMeshProUGUI _rewardText;
        [SerializeField] private TextMeshProUGUI _enmeiesInfo;
        [SerializeField] private TextMeshProUGUI _levelInfo;
        private Color32 _winColor;
        private Color32 _loseColor;
        public event UnityAction OnRestartClicked;
        public event UnityAction OnMetaClicked;
        public event UnityAction OnNextLvlClicked;

        public void Initialize()
        {
            _loseColor = new Color32(122, 122, 122, 255);
            _winColor = new Color32(220, 190, 100, 255);
            _restartButton.onClick.AddListener(Restart);
            _goMetatButton.onClick.AddListener(() => OnMetaClicked?.Invoke());
            _nextLvlButton.onClick.AddListener(() => OnNextLvlClicked?.Invoke());
        }

        public void SetEndLvlParams(LevelData levelData)
        {
            _rewardText.text = levelData.Reward.ToString();
            _enmeiesInfo.text = $"/{levelData.Enemies.Count.ToString()} врагов побеждено";
            _levelInfo.text = $"{levelData.Location} локация\n{levelData.LevelNumber} уровень";
        }

        public void ShowLoseWindow(int currentEnemy)
        {
            _loseLevelWindow.SetActive(true);
            _winLevelWindow.SetActive(false);
            gameObject.SetActive(true);
            _goMetatButton.image.color = _loseColor;
            _restartButton.image.color = _loseColor;
            _enmeiesInfo.text = currentEnemy.ToString() + _enmeiesInfo.text;
        }

        public void ShowWinWindow(int currentEnemy)
        {
            _loseLevelWindow.SetActive(false);
            _winLevelWindow.SetActive(true);
            gameObject.SetActive(true);
            _goMetatButton.image.color = _winColor;
            _restartButton.image.color = _winColor;
            _enmeiesInfo.text = currentEnemy.ToString() + _enmeiesInfo.text;
        }

        private void Restart()
        {
            OnRestartClicked?.Invoke();
            gameObject.SetActive(false);
        }
    }
}